#include "pch.h"
#include "SNCFileIO.h"
#include "Error.hpp"
#include "StringUtils.h"

#include <format>
#include<fstream>

SAPACORE::File::NetworkSetupDetails SAPACORE::File::Load(std::string path)
{
	std::ifstream ifile;
	ifile.open(path);
	if (!ifile.is_open()) { throw SapaException(std::format("Unable to open project file at {}", path)); }

	enum ReadMode{RM_HEADER, RM_INPUTS, RM_OUTPUTS, RM_NEURONS, RM_CIRCUIT};

	NetworkSetupDetails ret;

	ReadMode mode = RM_HEADER;
	std::string line;
	int lineno = 0;
	while (std::getline(ifile, line)) {
		++lineno;
		line = SAPACORE::StringUtils::strtrim(line);
		if (line.empty() || line[0] == '#') { continue; }
		std::vector<std::string> terms = SAPACORE::StringUtils::strsplit(line, ":,;<>");
		if (terms.size() == 0) { continue; }
		
		if (terms.size() == 1) {
			if (terms[0] == "$CIRCUIT") { mode = RM_CIRCUIT; continue; }
		}
		else if (terms.size() == 2) {
			if (terms[0] == "$TABLE") {
				if (terms[1] == "INPUTS") { mode = RM_INPUTS; continue; }
				else if (terms[1] == "OUTPUTS") { mode = RM_OUTPUTS; continue; }
				else if (terms[1] == "NEURONS") { mode = RM_NEURONS; continue; }
			}
		}

		switch (mode) {
			case RM_HEADER: continue;
			case RM_INPUTS: {
				if (terms.size() != 3) { throw SapaException(std::format("Invalid input definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				std::string name = terms[1];
				bool enabled = terms[2] == "True";
				ret.InputParam.push_back({ idx, name, enabled });
				}
			break;
			case RM_OUTPUTS: {
				if (terms.size() != 3) { throw SapaException(std::format("Invalid output definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				std::string name = terms[1];
				bool enabled = terms[2] == "True";
				ret.OutputParam.push_back({ idx, name, enabled });
			}
			break;
			case RM_NEURONS: {
				if (terms.size() != 5) { throw SapaException(std::format("Invalid input definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				std::string name = terms[1];
				float charge = stof(terms[2]);
				float bias = stof(terms[3]);
				float decay = stof(terms[4]);
				NeuronDef value = { idx, name, charge, bias, decay };
				ret.NeuronParam.push_back(value);
			}
			break;
			case RM_CIRCUIT:
				for (size_t i = 0; i < terms.size(); i += 3) {
					int sendIdx = stoi(terms[i]);
					float weight = stof(terms[i + 1]);
					int recIdx = stoi(terms[i]);
					ret.CircuitParam.push_back({sendIdx, weight, recIdx});
				}
			default:break;
		}
	}

	ifile.close();
	return ret;
}

std::string SAPACORE::File::Save(std::string path, SapaNetwork& network)
{
	return std::string();
}
