#include "pch.h"
#include "Prototypes.hpp"

SAPICORE_API SAPI::ProtoNeuron::ProtoNeuron(std::string id, float charge, float bias, float decay)
{
	this->id = id;
	this->charge = charge;
	this->bias = bias;
	this->decay = decay;
}

SAPICORE_API SAPI::ProtoRegion::ProtoRegion(std::string id)
{
	this->id = id;
}

SAPICORE_API SAPI::ProtoRegion::ProtoRegion()
{
	this->id = "";
}

SAPICORE_API bool SAPI::ProtoRegion::contains(std::string id)
{
	for (ProtoNeuron nrn : __neurons) {
		if (nrn.id == id) { return true; }
	}

	return false;
}

SAPICORE_API void SAPI::ProtoRegion::insert(const ProtoNeuron& nrn)
{
	if (contains(nrn.id)) { return; }
	__neurons.push_back(nrn);
}

SAPICORE_API SAPI::Blueprint::Blueprint(std::string regionId)
{
	__region = ProtoRegion(regionId);
}

SAPICORE_API void SAPI::Blueprint::InsertNeuron(const ProtoNeuron& proto)
{
	__region.insert(proto);
}

SAPICORE_API void SAPI::Blueprint::InsertNeurons(const ProtoNeuron* protos, UINT count)
{
	for (UINT i = 0; i < count; ++i) {
		ProtoNeuron nrn = protos[i];
		InsertNeuron(nrn);
	}
}
