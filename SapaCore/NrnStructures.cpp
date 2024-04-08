#include "pch.h"

#include "NrnStructures.hpp"
#include "Error.hpp"

SAPACORE::Neuron::Neuron(int index, float charge, float bias, float decay)
{
	__index = index;
	__charge = charge;
	__bias = bias;
	__decayRate = decay;
}

void SAPACORE::Neuron::Update()
{
}

void SAPACORE::IOCell::Update()
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

//Input/output values not used
SAPACORE::SapaNetwork::SapaNetwork(
	std::vector<InputDef> inputs, std::vector<OutputDef> outputs, 
	std::vector<NeuronDef> neurons)
{
	__numInputs = inputs.size();
	__numOutputs = outputs.size();
	__numNeurons = neurons.size();
	__running = false;

	__inputs = new Input*[__numInputs];
	for(size_t i=0; i<__numInputs; ++i)
	{
		const auto& [idx, name, enabled] = inputs[i];
		__inputs[i] = new Input(idx);
	}

	__outputs = new Output*[__numOutputs];
	for (size_t i = 0; i < __numOutputs; ++i) {
		const auto& [idx, name, enabled] = outputs[i];
		__outputs[i] = new Output(idx);
	}

	__network = new Neuron * [__numNeurons];
	for (size_t i = 0; i < __numNeurons; ++i) {
		const auto& [idx, name, charge, bias, decay] = neurons[i];
		__network[i] = new Neuron(idx, charge, bias, decay);
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

void SAPACORE::SapaNetwork::Update()
{
}

void SAPACORE::SapaNetwork::Start()
{
	__running = true;
}

void SAPACORE::SapaNetwork::Stop()
{
	__running = false;
}
