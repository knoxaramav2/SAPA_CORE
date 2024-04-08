#include "pch.h"

#include <Bits.h>

#include "NrnStructures.hpp"
#include "Error.hpp"
#include "SNCFileIO.h"

SAPACORE::Neuron::Neuron(int index, float charge, float bias, float decay)
{
	__index = index;
	__charge = charge;
	__bias = bias;
	__decayRate = decay;
}

void SAPACORE::Neuron::UpdateLocalState()
{
	__charge *= __decayRate;
}

void SAPACORE::Neuron::UpdateStimuliState()
{
	for (int i = 0; i < __dendrites.size(); ++i) {
		auto sig = __dendrites[i].sender->GetSignal();
		auto fx = get<1>(sig);
		auto active = get<0>(sig);
		//TODO Resolve transmitter effect
		__charge += (active * __dendrites[i].weight);
	}
}

void SAPACORE::IOCell::UpdateLocalState()
{
	__charge *= __decayRate;
}

void SAPACORE::IOCell::UpdateStimuliState()
{
}

SAPACORE::Output::Output(int index)
{
	__index = index;
	__charge = 0;
	__decayRate = 0;
	__bias = 0;
	__max = 1;
	__min = 0;
}

float SAPACORE::Output::Retreive()
{
	return this->__charge;
}

SAPACORE::Input::Input(int index)
{
	__index = index;
	__charge = 0;
	__decayRate = 0;
	__bias = 0;
	__max = 1;
	__min = 0;
}

void SAPACORE::Input::Excite(float value)
{
	__charge = __max;
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
		const auto& [idx, name, enabled] = inputs[i];
		__inputs[i] = new Input(idx);
	}
	minIdx = __inputs[0]->__index;
	maxIdx = __inputs[__numInputs - 1]->__index;
	__inIdxRng = std::make_tuple(minIdx, maxIdx);

	__outputs = new Output*[__numOutputs];
	for (size_t i = 0; i < __numOutputs; ++i) {
		const auto& [idx, name, enabled] = outputs[i];
		__outputs[i] = new Output(idx);
	}
	minIdx = __outputs[0]->__index;
	maxIdx = __outputs[__numOutputs - 1]->__index;
	__outIdxRng = std::make_tuple(minIdx, maxIdx);

	__network = new Neuron * [__numNeurons];
	for (size_t i = 0; i < __numNeurons; ++i) {
		const auto& [idx, name, charge, bias, decay] = neurons[i];
		__network[i] = new Neuron(idx, charge, bias, decay);
	}
	minIdx = __network[0]->__index;
	maxIdx = __network[__numNeurons - 1]->__index;
	__netIdxRng = std::make_tuple(minIdx, maxIdx);

	for (size_t i = 0; i < __dendrites.size(); ++i) {
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
	for (int i = 0; i < __numInputs; ++i) { __inputs[i]->UpdateLocalState(); }
	for (int i = 0; i < __numNeurons; ++i) { __network[i]->UpdateLocalState(); }
	for (int i = 0; i < __numOutputs; ++i) { __outputs[i]->UpdateLocalState(); }
}

void SAPACORE::SapaNetwork::StimuliUpdatePass()
{
	for (int i = 0; i < __numNeurons; ++i) {
		__network[i]->UpdateStimuliState();
	}
}

void SAPACORE::SapaNetwork::InputUpdatePass()
{
	for (int i = 0; i < __numInputs; ++i) {
		__inputs[i]->UpdateStimuliState();
	}
}

void SAPACORE::SapaNetwork::OutputUpdatePass()
{
	for (int i = 0; i < __numOutputs; ++i) {
		__outputs[i]->UpdateStimuliState();
	}
}

void SAPACORE::QCell::AddConnection(QCell* sender, float weight)
{
	auto i = find_if(__dendrites.begin(), __dendrites.end(), [sender](Dendrite x) {return x.sender == sender; });
	if (i != __dendrites.end()) { return; }
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
