#include "pch.h"

#include "NrnStructures.hpp"

SAPA::Neuron::Neuron()
{
	charge = 0;
	bias = 0;
	decay = 0;
}

bool SAPA::Neuron::receive()
{
	return false;
}

void SAPA::Neuron::activate()
{
}

SAPA::NRegion::NRegion()
{
}

SAPA::NComplex::NComplex()
{
}
