#include "pch.h"

#include <Bits.h>

#include "NrnStructures.hpp"
#include "Error.hpp"
#include "SNCFileIO.h"
#include "iostream"

SAPACORE::Neuron::Neuron(int index, float charge, float bias, float decay, UINT64 transcode)
{
	__index = index;
	__charge = charge;
	__bias = bias;
	__decayRate = decay;
	__transCode = transcode;
}

void SAPACORE::Neuron::UpdateLocalState()
{
	__charge *= __decayRate;
}

void SAPACORE::Neuron::UpdateStimuliState()
{
	for (size_t i = 0; i < __dendrites.size(); ++i) {
		//TODO Resolve transmitter effect
		__charge += __dendrites[i].GetCharge();
	}
}

void SAPACORE::IOCell::UpdateLocalState()
{
	__charge *= __decayRate;
}

void SAPACORE::IOCell::UpdateStimuliState()
{
	if (!__enabled) { return; }
	for (size_t i = 0; i < __dendrites.size(); ++i) {
		__charge += __dendrites[i].GetCharge();
	}
}

SAPACORE::Output::Output(int index, bool enabled, float decay)
{
	__index = index;
	__charge = 0;
	__enabled = enabled;
	__decayRate = decay;
	__bias = 1;
	__max = 1;
	__min = 0;
}

float SAPACORE::Output::Retreive()
{
	return this->__charge;
}

SAPACORE::Input::Input(int index, bool enabled, float decay)
{
	__index = index;
	__charge = 0;
	__enabled = enabled;
	__decayRate = decay;
	__bias = 1;
	__max = 1;
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
		__network[idx - get<0>(__netIdxRng)] :
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

#define BC(x) (get<0>(x) ? '+':'-')
void SAPACORE::SapaNetwork::DevPrint()
{
	auto i0 = __inputs[0]->GetSignal();
	auto i0q = __inputs[0]->__charge;
	auto i1 = __inputs[1]->GetSignal();
	auto i1q = __inputs[1]->__charge;
	auto i2 = __inputs[2]->GetSignal();
	auto i2q = __inputs[2]->__charge;
	auto i3 = __inputs[3]->GetSignal();
	auto i3q = __inputs[3]->__charge;

	auto o0 = __outputs[0]->GetSignal();
	auto o0q = __outputs[0]->__charge;
	auto o1 = __outputs[1]->GetSignal();
	auto o1q = __outputs[1]->__charge;
	auto o2 = __outputs[2]->GetSignal();
	auto o2q = __outputs[2]->__charge;
	auto o3 = __outputs[3]->GetSignal();
	auto o3q = __outputs[3]->__charge;

	auto n0 = __network[0]->GetSignal();
	auto n0q = __network[0]->__charge;
	auto n1 = __network[1]->GetSignal();
	auto n1q = __network[1]->__charge;
	auto n2 = __network[2]->GetSignal();
	auto n2q = __network[2]->__charge;
	auto n3 = __network[3]->GetSignal();
	auto n3q = __network[3]->__charge;
	auto n4 = __network[0]->GetSignal();
	auto n4q = __network[0]->__charge;
	auto n5 = __network[1]->GetSignal();
	auto n5q = __network[1]->__charge;
	auto n6 = __network[2]->GetSignal();
	auto n6q = __network[2]->__charge;
	auto n7 = __network[3]->GetSignal();
	auto n7q = __network[3]->__charge;

	printf("I0 %.3f (%c)  %.3f(%c)   %.3f(%c)   O0 %.3f (%c)\r\n", i0q, BC(i0), n0q, BC(n0), n4q, BC(n4), o0q, BC(o0));
	printf("I1 %.3f (%c)  %.3f(%c)   %.3f(%c)   O1 %.3f (%c)\r\n", i1q, BC(i1), n1q, BC(n1), n5q, BC(n5), o1q, BC(o1));
	printf("I2 %.3f (%c)  %.3f(%c)   %.3f(%c)   O2 %.3f (%c)\r\n", i2q, BC(i2), n2q, BC(n2), n6q, BC(n6), o2q, BC(o2));
	printf("I3 %.3f (%c)  %.3f(%c)   %.3f(%c)   O3 %.3f (%c)\r\n", i3q, BC(i3), n3q, BC(n3), n7q, BC(n7), o3q, BC(o3));
	printf("__________\r\n");
}

//Input/output values not used
SAPACORE::SapaNetwork::SapaNetwork(
	std::vector<InputDef> inputs, std::vector<OutputDef> outputs, 
	std::vector<NeuronDef> neurons,
	std::vector<CircuitDef> connections)
{
	__numInputs = inputs.size();
	__numOutputs = outputs.size();
	__numNeurons = neurons.size();

	__inputs = new Input*[__numInputs];
	int minIdx, maxIdx;
	for(size_t i=0; i<__numInputs; ++i)
	{
		const auto& [idx, name, enabled, decay] = inputs[i];
		__inputs[i] = new Input(idx, enabled, decay);
	}
	minIdx = __inputs[0]->__index;
	maxIdx = __inputs[__numInputs - 1]->__index;
	__inIdxRng = std::make_tuple(minIdx, maxIdx);

	__outputs = new Output*[__numOutputs];
	for (size_t i = 0; i < __numOutputs; ++i) {
		const auto& [idx, name, enabled, decay] = outputs[i];
		__outputs[i] = new Output(idx, enabled, decay);
	}
	minIdx = __outputs[0]->__index;
	maxIdx = __outputs[__numOutputs - 1]->__index;
	__outIdxRng = std::make_tuple(minIdx, maxIdx);

	__network = new Neuron * [__numNeurons];
	for (size_t i = 0; i < __numNeurons; ++i) {
		const auto& [idx, name, charge, bias, decay, transcode] = neurons[i];
		__network[i] = new Neuron(idx, charge, bias, decay, transcode);
	}
	minIdx = __network[0]->__index;
	maxIdx = __network[__numNeurons - 1]->__index;
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
	for (size_t i = 0; i < __numNeurons; ++i) { delete __network[i]; }
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
	for (size_t i = 0; i < __numNeurons; ++i) { __network[i]->UpdateLocalState(); }
	for (size_t i = 0; i < __numOutputs; ++i) { __outputs[i]->UpdateLocalState(); }
}

void SAPACORE::SapaNetwork::StimuliUpdatePass()
{
	for (size_t i = 0; i < __numNeurons; ++i) {
		__network[i]->UpdateStimuliState();
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
	return std::tuple<bool, UINT32>(__charge>=__bias, __transmitter);
}

SAPACORE::SapaDiagnostic::SapaDiagnostic(SapaNetwork& network)
{
	__network = &network;
}

void SAPACORE::SapaDiagnostic::PrintActivity()
{
	auto i0 = __network->__inputs[0]->GetSignal();
	auto i0q = __network->__inputs[0]->__charge;
	auto i1 = __network->__inputs[1]->GetSignal();
	auto i1q = __network->__inputs[1]->__charge;
	auto i2 = __network->__inputs[2]->GetSignal();
	auto i2q = __network->__inputs[2]->__charge;
	auto i3 = __network->__inputs[3]->GetSignal();
	auto i3q = __network->__inputs[3]->__charge;

	auto o0 = __network->__outputs[0]->GetSignal();
	auto o0q = __network->__outputs[0]->__charge;
	auto o1 = __network->__outputs[1]->GetSignal();
	auto o1q = __network->__outputs[1]->__charge;
	auto o2 = __network->__outputs[2]->GetSignal();
	auto o2q = __network->__outputs[2]->__charge;
	auto o3 = __network->__outputs[3]->GetSignal();
	auto o3q = __network->__outputs[3]->__charge;

	auto n0 = __network->__network[0]->GetSignal();
	auto n0q = __network->__network[0]->__charge;
	auto n1 = __network->__network[1]->GetSignal();
	auto n1q = __network->__network[1]->__charge;
	auto n2 = __network->__network[2]->GetSignal();
	auto n2q = __network->__network[2]->__charge;
	auto n3 = __network->__network[3]->GetSignal();
	auto n3q = __network->__network[3]->__charge;
	auto n4 = __network->__network[0]->GetSignal();
	auto n4q = __network->__network[0]->__charge;
	auto n5 = __network->__network[1]->GetSignal();
	auto n5q = __network->__network[1]->__charge;
	auto n6 = __network->__network[2]->GetSignal();
	auto n6q = __network->__network[2]->__charge;
	auto n7 = __network->__network[3]->GetSignal();
	auto n7q = __network->__network[3]->__charge;

	printf("I0 %.3f (%c)  %.3f(%c)   %.3f(%c)   O0 %.3f (%c)\r\n", i0q, get<0>(i0), n0q, get<0>(n0), n4q, get<0>(n4), o0q, get<0>(o0));
	printf("I0 %.3f (%c)  %.3f(%c)   %.3f(%c)   O0 %.3f (%c)\r\n", i1q, get<0>(i1), n1q, get<0>(n1), n5q, get<0>(n5), o1q, get<0>(o1));
	printf("I0 %.3f (%c)  %.3f(%c)   %.3f(%c)   O0 %.3f (%c)\r\n", i2q, get<0>(i2), n2q, get<0>(n2), n6q, get<0>(n6), o2q, get<0>(o2));
	printf("I0 %.3f (%c)  %.3f(%c)   %.3f(%c)   O0 %.3f (%c)\r\n", i3q, get<0>(i3), n3q, get<0>(n3), n7q, get<0>(n7), o3q, get<0>(o3));

	return;
	/*auto i0 = __network->__inputs[0]->GetSignal();
	auto iq = __network->__inputs[0]->__charge;
	auto o0 = __network->__outputs[0]->GetSignal();
	auto oq = __network->__outputs[0]->__charge;
	auto n0 = __network->__network[0]->GetSignal();
	auto nq = __network->__network[0]->__charge;
	std::cout << 
		"I0: " << get<0>(i0) << " " << get<1>(i0) << " " << iq << " || " <<
		"N0: " << get<0>(n0) << " " << get<1>(n0) << " " << nq << " || " <<
		"I0: " << get<0>(o0) << " " << get<1>(o0) << " " << oq << " || "
		<< "______________________" << std::endl;
	
	
	
	return;
	std::string inputs;
	std::string outputs;
	std::string neurons;

	for (size_t i = 0; i < __network->__numInputs; ++i) {
		inputs += get<0>(__network->__inputs[i]->GetSignal()) ? '+':'-';
	}

	for (size_t i = 0; i < __network->__numOutputs; ++i) {
		outputs += get<0>(__network->__outputs[i]->GetSignal()) ? '+' : '-';
		if (i % 20 == 0) { outputs += "\r\n"; }
	}

	for (size_t i = 0; i < __network->__numNeurons; ++i) {
		neurons += get<0>(__network->__network[i]->GetSignal()) ? '+' : '-';
	}

	printf("\r\nSCAN\nIN\r\n%s\r\nNRN\r\n%s\r\nOUT\r\n%s\r\n________________________________\r\n", inputs.c_str(), neurons.c_str(), outputs.c_str());*/
}

float SAPACORE::Dendrite::GetCharge()
{
	return weight*get<0>(sender->GetSignal());
}
