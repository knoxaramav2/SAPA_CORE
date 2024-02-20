#pragma once

#include "pch.h"
#include "commondef.hpp"

#include <string>
#include <vector>

namespace SAPI {
	
	struct ProtoNeuron {
		std::string id;
		float charge;
		float bias;
		float decay;

		std::vector<std::string> connections;

		SAPICORE_API ProtoNeuron(std::string id, float charge, float bias, float decay);
	};

	struct ProtoRegion {
		std::string id;
		std::vector<ProtoNeuron> __neurons;

		SAPICORE_API ProtoRegion(std::string id);
		SAPICORE_API ProtoRegion();

		SAPICORE_API bool contains(std::string id);
		SAPICORE_API void insert(const ProtoNeuron& nrn);
	};

	struct Blueprint {
		SAPICORE_API Blueprint(std::string regionId);
		SAPICORE_API void InsertNeuron(const ProtoNeuron& proto);
		SAPICORE_API void InsertNeurons(const ProtoNeuron* protos, UINT count);
	private:
		ProtoRegion __region;
	};

}