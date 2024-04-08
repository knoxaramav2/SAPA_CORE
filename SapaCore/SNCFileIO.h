#pragma once
#include <string>
#include <vector>
#include "NrnStructures.hpp"

namespace SAPACORE::File {

	struct NetworkSetupDetails {
		std::vector<InputDef> InputParam;
		std::vector<OutputDef> OutputParam;
		std::vector<NeuronDef> NeuronParam;
	};

	static NetworkSetupDetails Load(std::string path);
	static std::string Save(std::string path, SapaNetwork& network);
}