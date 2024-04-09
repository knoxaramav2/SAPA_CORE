#include "pch.h"
#include "NetworkIOAdapter.h"

SAPACORE::NetworkIOAdapter::NetworkIOAdapter(SapaNetwork& network, InputFnc ifnc, OutputFnc ofnc):
	__inputs(network.__numInputs, ifnc, network.__inputs), __outputs(network.__numOutputs, ofnc, network.__outputs)
{
	__neurons = &network;
}

SAPACORE::InputAdapter* SAPACORE::NetworkIOAdapter::GetInputAdapter()
{
	return &__inputs;
}

SAPACORE::OutputAdapter* SAPACORE::NetworkIOAdapter::GetOutputAdapter()
{
	return &__outputs;
}

SAPACORE::InputAdapter::InputAdapter(int size, InputFnc ifnc, Input** inputs)
{
	__size = size;
	__inputFunc = ifnc;
	__inputs = inputs;
}

SAPACORE::InputAdapter::~InputAdapter()
{
}

void SAPACORE::InputAdapter::SetValue(float value, int idx)
{
	if (idx >= __size) { return; }
	__inputs[idx]->Excite(value);
}

void SAPACORE::InputAdapter::Update()
{
	__inputFunc(this);
}

SAPICORE_API int SAPACORE::InputAdapter::Size()
{
	return __size;
}

SAPACORE::OutputAdapter::OutputAdapter(int size, OutputFnc ofnc, Output** outputs)
{
	__size = size;
	__outputFunc = ofnc;
	__outputs = outputs;
}

SAPACORE::OutputAdapter::~OutputAdapter()
{
	
}

float SAPACORE::OutputAdapter::GetValue(int idx)
{
	if (idx >= __size) { return -1; }
	return __outputs[idx]->Retreive();
}

void SAPACORE::OutputAdapter::Update()
{
	__outputFunc(this);
}

SAPICORE_API int SAPACORE::OutputAdapter::Size()
{
	return __size;
}
