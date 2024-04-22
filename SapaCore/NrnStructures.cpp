#include "pch.h"

#include <Bits.h>
#include <format>
#include <iostream>
#include <math.h>

#include "NrnStructures.hpp"
#include "Error.hpp"
#include "SNCFileIO.h"
#include "FileUtil.h"

float SAPACORE::Neuron::calc_dndt(float v, float n)
{
	return (
		(((0.01f*(v+55.0f))/(1.0f- (float)exp((-(v+50.0f)/10.0f))))*(1-n)) -
		((.125f*exp((-(v+60.0f))/80.0f))*n)
		);
}

float SAPACORE::Neuron::calc_dhdt(float v, float h)
{
	return (
		((0.07f*exp(-5.0f*(v+60)))*(1-h)) -
		((1.0f/(1.0f+ (float)exp(-.1*(v+30))))*h)
		);
}

float SAPACORE::Neuron::calc_dmdt(float v, float m)
{
	return (
		((.1f*(v+35.0f)/(1-(float)exp((-(v+35.0f))/10.0f)*(1-m)))) -
		((4.0f*exp(-0.0556f*(v+60.0f)))*m)
		);
}

SAPACORE::Neuron::Neuron(int index,
	float cm, float gna, float gk, float gl,
	IonState* interIons, IonState* intraIons,
	CircuitConfig*cfg)
{
	__index = index;
	__rate_n = 0.40f;
	__rate_h = 0.90f;
	__rate_m = 0.05f;
	__Cm = cm;
	__GNa = gna;
	__GK = gk;
	__GL = gl;
	__extCharge = 0.0f;
	__refactory = false;
	__intercell = interIons;
	__intracell = intraIons;
	__circuitCfg = cfg;
}

void SAPACORE::Neuron::UpdateLocalState()
{
	const float DT = 0.4f;

	if (__refactory) { __extCharge = 0; }

	float dn = calc_dndt(__charge, __rate_n);
	float dh = calc_dhdt(__charge, __rate_h);
	float dm = calc_dmdt(__charge, __rate_m);

	float gNa = __GNa * pow(__rate_m, 3) * __rate_h;
	float gK = __GK * pow(__rate_m, 4);
	float gl = __GL;

	float INa = __GNa * (__charge - __circuitCfg->ENa);
	float Ik = __GK * (__charge - __circuitCfg->EK);
	float Il = __GL * (__charge - __circuitCfg->EL);

	float dv = ((1 / __Cm) * (__extCharge - (INa + Ik + Il))) * DT;

	__rate_n += dn;
	__rate_h += dh;
	__rate_m += dm;
	__charge += dv;

	__refactory = (!__refactory && __charge >= __threshold);

	if (__refactory && __charge < __threshold){}
}

void SAPACORE::Neuron::UpdateStimuliState()
{
	for (size_t i = 0; i < __dendrites.size() && !__refactory; ++i) {
		//TODO Resolve transmitter effect
		__extCharge += __dendrites[i].GetCharge();
	}
}

std::tuple<bool, UINT32> SAPACORE::Neuron::GetSignal()
{
	return std::tuple<bool, UINT32>(!__refactory && __charge >= __threshold, __transmitter);
}

float SAPACORE::Dendrite::GetCharge()
{
	return weight * get<0>(sender->GetSignal());
}

void SAPACORE::IOCell::UpdateLocalState()
{
	
}

void SAPACORE::IOCell::UpdateStimuliState()
{
	if (!__enabled) { return; }
	for (size_t i = 0; i < __dendrites.size(); ++i) {
		__charge += __dendrites[i].GetCharge();
	}
}

float SAPACORE::IOCell::GetCharge()
{
	return __charge;
}

SAPACORE::Output::Output(int index, bool enabled)
{
	__index = index;
	__charge = 0;
	__enabled = enabled;
	__threshold = .9f;
	__max = 1;
	__min = 0;
}

float SAPACORE::Output::Retreive()
{
	return this->__charge;
}

SAPACORE::Input::Input(int index, bool enabled)
{
	__index = index;
	__charge = 0;
	__extCharge = 0;
	__enabled = enabled;
	__threshold = 1;
	__max = 100;
	__min = 0;
}

void SAPACORE::Input::Excite(float value)
{
	__charge = value;
}

void SAPACORE::Input::AddConnection(QCell* sender, float weight)
{
	throw SapaException("AddConnection not allowed on input cells");
}

void SAPACORE::Input::PruneConnection(QCell* sender)
{
	throw SapaException("PruneConnection not allowed on input cells");
}

SAPACORE::Neuron* SAPACORE::SapaNetwork::__findNeuronByIdx(int idx)
{
	return INRANGE(__netIdxRng, idx) ?
		__neurons[idx - get<0>(__netIdxRng)] :
		nullptr;
}

SAPACORE::Input* SAPACORE::SapaNetwork::__findInputByIdx(int idx)
{
	return INRANGE(__inIdxRng, idx) ?
		__inputs[idx - get<0>(__inIdxRng)] :
		nullptr;
}

SAPACORE::Output* SAPACORE::SapaNetwork::__findOutputByIdx(int idx)
{
	return INRANGE(__outIdxRng, idx) ?
		__outputs[idx - get<0>(__outIdxRng)] :
		nullptr;
}

SAPACORE::QCell* SAPACORE::SapaNetwork::__findByIdx(int idx)
{
	QCell* ret = __findNeuronByIdx(idx);
	ret = ret == nullptr ? __findInputByIdx(idx) : ret;
	ret = ret == nullptr ? __findOutputByIdx(idx): ret;

	return ret;
}

#define BC(x) (x ? '+':'-')
void SAPACORE::SapaNetwork::DevPrint()
{
	auto inputs = __inputs;
	auto outputs = __outputs;
	auto neurons = __neurons;
	printf("\r\033[%dA", 9);

	for (size_t i = 0; i < __numInputs; ++i) {
		auto inp = __inputs[i];
		auto sig = inp->GetSignal();
		auto act = get<0>(sig);
		printf("I%-2d %6.3f (%c) ", inp->__index, inp->__charge, BC(act));
	}

	printf("\r\n");

	for (size_t i = 0; i < __numNeurons; ++i) {
		auto nrn = __neurons[i];
		auto sig = nrn->GetSignal();
		auto act = get<0>(sig);
		printf("N%-2d %6.3f (%c) ", nrn->__index, nrn->__charge, BC(act));
		if ((i + 1) % 5 == 0) 
			{ printf("\r\n"); }
	}

	printf("\r\n");

	for (size_t i = 0; i < __numOutputs; ++i) {
		auto out = __outputs[i];
		auto sig = out->GetSignal();
		auto act = get<0>(sig);
		printf("O%2d %6.3f (%c) ", out->__index, out->__charge, BC(act));
	}

	printf("\r\n__________________________\r\n");
}

//Input/output values not used
SAPACORE::SapaNetwork::SapaNetwork(
	std::vector<InputDef> inputs, std::vector<OutputDef> outputs, 
	std::vector<NeuronDef> neurons,
	std::vector<NetworkDef> connections,
	std::vector<IonDef> ions,
	std::vector<CircuitDef> circuits)
{
	__numInputs = inputs.size();
	__numOutputs = outputs.size();
	__numNeurons = neurons.size();
	__numIonStates = ions.size();
	__numCircConfigs = circuits.size();

	__circuitConfigs = new CircuitConfig * [__numCircConfigs];
	for (size_t i = 0; i < __numCircConfigs; ++i) {
		const auto& [idx, name, rst_m, rst_h, rst_n,
		rest, vm, ena, ek, el] = circuits[i];
		if ((size_t)idx >= __numCircConfigs) { throw SapaException("Circuit definition index out of range"); }
		__circuitConfigs[idx] = new CircuitConfig(name.c_str(), rst_m, rst_h, rst_n, rest, vm, ena, ek, el);
	}

	__ionStates = new IonState*[__numIonStates ];
	for (size_t i = 0; i < __numIonStates; ++i) {
		const auto& [idx, nap, nac, kp, kc, cap, cac, clp, clc] = ions[i];
		if ((size_t)idx >= __numIonStates) { throw SapaException("Ion definition index out of range"); }
		__ionStates[idx] = new IonState(nap, nac, kp, kc, cap, cac, clp, clc);
	}

	__inputs = new Input*[__numInputs];
	int minIdx, maxIdx;
	for(size_t i=0; i<__numInputs; ++i)
	{
		const auto& [idx, name, enabled, decay] = inputs[i];
		__inputs[i] = new Input(idx, enabled);
	}
	minIdx = __inputs[0]->__index;
	maxIdx = __inputs[__numInputs - 1]->__index;
	__inIdxRng = std::make_tuple(minIdx, maxIdx);

	__outputs = new Output*[__numOutputs];
	for (size_t i = 0; i < __numOutputs; ++i) {
		const auto& [idx, name, enabled, decay] = outputs[i];
		__outputs[i] = new Output(idx, enabled);
	}
	minIdx = __outputs[0]->__index;
	maxIdx = __outputs[__numOutputs - 1]->__index;
	__outIdxRng = std::make_tuple(minIdx, maxIdx);

	__neurons = new Neuron * [__numNeurons];
	for (size_t i = 0; i < __numNeurons; ++i) {
		const auto& [idx, ccidx, cm, gna, gk, gl, ioni, ione] = neurons[i];
		IonState* inter = __ionStates[ioni];
		IonState* intra = __ionStates[ione];
		CircuitConfig* cfg = __circuitConfigs[ccidx];
		__neurons[i] = new Neuron(idx, cm, gna, gk, gl, inter, intra, cfg);
	}
	minIdx = __neurons[0]->__index;
	maxIdx = __neurons[__numNeurons - 1]->__index;
	__netIdxRng = std::make_tuple(minIdx, maxIdx);

	for (size_t i = 0; i < connections.size(); ++i) {
		const auto& [sidx, weight, ridx] = connections[i];
		QCell* sender = __findByIdx(sidx);
		QCell* receiver = __findByIdx(ridx);
		receiver->AddConnection(sender, weight);
	}
}

SAPACORE::SapaNetwork::~SapaNetwork()
{
	for (size_t i = 0; i < __numInputs; ++i) { delete __inputs[i]; }
	for (size_t i = 0; i < __numOutputs; ++i) { delete __outputs[i]; }
	for (size_t i = 0; i < __numNeurons; ++i) { delete __neurons[i]; }
}

float SAPACORE::SapaNetwork::GetOutput(size_t index)
{
	if (index >= __numOutputs) { throw SapaException("Get output index out of bounds."); }
	return __outputs[index]->Retreive();
}

void SAPACORE::SapaNetwork::SetInput(size_t index, float value)
{
	if (index >= __numInputs) {
		throw SapaException("Set input index out of bounds.");
	}
	__inputs[index]->Excite(value);
}

void SAPACORE::SapaNetwork::LocalUpdatePass()
{
	for (size_t i = 0; i < __numInputs; ++i) { __inputs[i]->UpdateLocalState(); }
	for (size_t i = 0; i < __numNeurons; ++i) { __neurons[i]->UpdateLocalState(); }
	for (size_t i = 0; i < __numOutputs; ++i) { __outputs[i]->UpdateLocalState(); }
}

void SAPACORE::SapaNetwork::StimuliUpdatePass()
{
	for (size_t i = 0; i < __numNeurons; ++i) {
		__neurons[i]->UpdateStimuliState();
	}
}

void SAPACORE::SapaNetwork::OutputUpdatePass()
{
	for (size_t i = 0; i < __numOutputs; ++i) {
		__outputs[i]->UpdateStimuliState();
	}
}

void SAPACORE::QCell::AddConnection(QCell* sender, float weight)
{
	if (dynamic_cast<Input*>(this) != nullptr) {
		printf("ERR");
	}
	auto i = find_if(__dendrites.begin(), __dendrites.end(), [sender](Dendrite x) {return x.sender == sender; });
	if (i != __dendrites.end()) { nullptr; }
	__dendrites.push_back(Dendrite(sender, weight));
}

void SAPACORE::QCell::PruneConnection(QCell* sender)
{
	auto i = find_if(__dendrites.begin(), __dendrites.end(), [sender](Dendrite x) {return x.sender == sender; });
	if (i == __dendrites.end()) { return; }
	__dendrites.erase(i);
}

std::tuple<bool, UINT32> SAPACORE::QCell::GetSignal()
{
	return std::tuple<bool, UINT32>(__charge>=__threshold, __transmitter);
}

float SAPACORE::QCell::GetCharge()
{
	return __charge;
}

SAPACORE::SapaDiagnostic::SapaDiagnostic(SapaNetwork& network)
{
	__network = &network;
	
	auto baseDir = GetEXEPath();
	__diagDir = baseDir + L"/Data";
	__snapSlice = 0;
}

#define CB(x)(x?'+':'-')
void SAPACORE::SapaDiagnostic::PrintActivity()
{
	auto inputs = __network->__inputs;
	auto outputs = __network->__outputs;
	auto neurons = __network->__neurons;

	for (size_t i = 0; i < __network->__numInputs; ++i) {
		auto inp = __network->__inputs[i];
		auto sig = inp->GetSignal();
		auto act = get<0>(sig);
		printf("I%-2d %.3f (%c) | ", inp->__index, inp->__charge, CB(act));
	}

	printf("\r\n");

	for (size_t i = 0; i < __network->__numNeurons; ++i) {
		auto nrn = __network->__neurons[i];
		auto sig = nrn->GetSignal();
		auto act = get<0>(sig);
		printf("N%-2d %.3f (%c) | ", nrn->__index, nrn->__charge, CB(act));
	}
	
	printf("\r\n");

	for (size_t i = 0; i < __network->__numOutputs; ++i) {
		auto out = __network->__outputs[i];
		auto sig = out->GetSignal();
		auto act = get<0>(sig);
		printf("O%-2d %.3f (%c) | ", out->__index, out->__charge, CB(act));
	}

	printf("\r\n__________________________\r\n");
}

SAPICORE_API void SAPACORE::SapaDiagnostic::Snapshot()
{
	std::vector<float> data;
	static unsigned sliceStep = 0;

	if (++sliceStep%(__snapSlice+1) != 0) { return; }
	
	for (size_t i = 0; i < __network->__numInputs; ++i) {
		data.push_back(__network->__inputs[i]->GetCharge());
	}

	for (size_t i = 0; i < __network->__numOutputs; ++i) {
		data.push_back(__network->__outputs[i]->GetCharge());
	}

	for (size_t i = 0; i < __network->__numNeurons; ++i) {
		data.push_back(__network->__neurons[i]->GetCharge());
	}

	__snapVals.push_back(data);
}

SAPICORE_API std::string SAPACORE::SapaDiagnostic::GetCSV()
{
	std::string ret;
	for (size_t i = 0; i < __network->__numInputs; ++i) {
		ret += std::format("I{},", i);
	}
	for (size_t i = 0; i < __network->__numOutputs; ++i) {
		ret += std::format("O{},", i);
	}
	for (size_t i = 0; i < __network->__numNeurons; ++i) {
		ret += std::format("N{},", i);
	}
	ret.pop_back();
	ret += "\r\n";
	for (auto& line : __snapVals) {
		for (float val : line) {
			ret += std::format("{},", val);
		}
		ret.pop_back();
		ret += "\r\n";
	}
	ret += "\r\n";
	return ret;
}

SAPICORE_API void SAPACORE::SapaDiagnostic::SaveCSV()
{
	SaveCSV(__diagDir+L"/run_data.csv");
}

SAPICORE_API void SAPACORE::SapaDiagnostic::SaveCSV(String path)
{
	AssurePath(path);
	SaveFile(path, GetCSV());
}

SAPICORE_API void SAPACORE::SapaDiagnostic::SetSnapSlice(unsigned size)
{
	__snapSlice = size;
}

SAPICORE_API SAPACORE::CircuitConfig::CircuitConfig(
	const char* name,
	float rstm, float rsth, float rstn,
	float rest, float vm, 
	float ena, float ek, float el):
	C_RST_M(rstm), C_RST_H(rsth), C_RST_N(rstn),
	C_REST(rest), C_Vm(vm),
	ENa(ena), EK(ek), EL(el){
		CircName = name;
	}

SAPICORE_API SAPACORE::CircuitConfig::CircuitConfig() :CircuitConfig(
	"Default",
	0.05f, 0.90f, 0.40f,
	-65.0f, -70.0f, 
	55.17f, -72.14f, -49.42f 
)
{}
