#pragma once
#include <string>
#include <vector>
#include "commondef.hpp"
#include "NrnStructures.hpp"

namespace SAPACORE::File {

	extern struct NetworkSetupDetails {
		std::vector<InputDef> InputParam;
		std::vector<OutputDef> OutputParam;
		std::vector<NeuronDef> NeuronParam;
	};

	extern "C" SAPICORE_API NetworkSetupDetails Load(std::string path);
	extern "C" SAPICORE_API std::string Save(std::string path, SapaNetwork& network);
}