#pragma once
#include <string>
#include <vector>
#include "commondef.hpp"
#include "NrnStructures.hpp"

namespace SAPACORE::File {

	struct NetworkSetupDetails {
		std::vector<InputDef> InputParam;
		std::vector<OutputDef> OutputParam;
		std::vector<NeuronDef> NeuronParam;
		std::vector<CircuitDef> CircuitParam;
	};

	extern SAPICORE_API NetworkSetupDetails Load(std::string path);
	extern SAPICORE_API std::string Save(std::string path, SapaNetwork& network);
}