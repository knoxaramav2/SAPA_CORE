#include "pch.h"
#include "NetworkIOAdapter.h"

SAPACORE::NetworkIOAdapter::NetworkIOAdapter(SapaNetwork& network, InputFnc ifnc, OutputFnc ofnc):
	__inputs(network.__numInputs, ifnc), __outputs(__network->__numOutputs, ofnc)
{
	__network = &network;
}

SAPACORE::InputAdapter* SAPACORE::NetworkIOAdapter::GetInputAdapter()
{
	return &__inputs;
}

SAPACORE::OutputAdapter* SAPACORE::NetworkIOAdapter::GetOutputAdapter()
{
	return &__outputs;
}

SAPACORE::InputAdapter::InputAdapter(int size, InputFnc ifnc)
{
	__size = size;
	__inputThread = ifnc;
	__values = new float[size];
	for (int i = 0; i < size; ++i) { __values = 0; }
}

SAPACORE::InputAdapter::~InputAdapter()
{
	delete __values;
}

void SAPACORE::InputAdapter::SetValue(float value, int idx)
{
	if (idx >= __size) { return; }
	__values[idx] = value;
}

SAPACORE::OutputAdapter::OutputAdapter(int size, OutputFnc ofnc)
{
	__size = size;
	__outputThread = ofnc;
	__values = new float[size];
	for (int i = 0; i < size; ++i) { __values = 0; }
}

SAPACORE::OutputAdapter::~OutputAdapter()
{
	delete __values;
}

float SAPACORE::OutputAdapter::GetValue(float value, int idx)
{
	if (idx >= __size) { return -1; }
	return __values[idx];
}
