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

	enum ReadMode{RM_HEADER, RM_INPUTS, RM_OUTPUTS, RM_NEURONS, RM_NETWORK, RM_CIRCUIT, RM_IONS};

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
			if (terms[0] == "$NETWORK") { mode = RM_NETWORK; continue; }
		}
		else if (terms.size() == 2) {
			if (terms[0] == "$TABLE") {
				if (terms[1] == "INPUTS") { mode = RM_INPUTS; continue; }
				else if (terms[1] == "OUTPUTS") { mode = RM_OUTPUTS; continue; }
				else if (terms[1] == "NEURONS") { mode = RM_NEURONS; continue; }
				else if (terms[1] == "CIRCUIT") { mode = RM_CIRCUIT; continue; }
				else if (terms[1] == "IONS") { mode = RM_IONS; continue; }
			}
		}

		switch (mode) {
			case RM_HEADER: continue;
			case RM_INPUTS: {
				if (terms.size() != 4) { throw SapaException(std::format("Invalid input definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				std::string name = terms[1];
				bool enabled = terms[2] == "True";
				float decay = stof(terms[3]);
				ret.InputParam.push_back({ idx, name, enabled, decay });
				}
			break;
			case RM_OUTPUTS: {
				if (terms.size() != 4) { throw SapaException(std::format("Invalid output definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				std::string name = terms[1];
				bool enabled = terms[2] == "True";
				float decay = stof(terms[3]);
				ret.OutputParam.push_back({ idx, name, enabled, decay });
			}
			break;
			case RM_NEURONS: {
				if (terms.size() != 10) { throw SapaException(std::format("Invalid input definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				int ionInter = stoi(terms[1]);
				int ionIntra = stoi(terms[2]);
				std::string name = terms[3];
				float charge = stof(terms[4]);
				float thresh = stof(terms[5]);
				float resistance = stof(terms[6]);
				float resting = stof(terms[7]);
				char* ss;
				UINT64 transcode = strtoul(terms[8].c_str(), &ss, 10);
				bool refactory = terms[9] == "True";
				NeuronDef value = { idx, ionInter, ionIntra, name, charge, thresh, resistance, resting, transcode, refactory };
				ret.NeuronParam.push_back(value);
			}
			break;
			case RM_CIRCUIT: {
				int idx = stoi(terms[0]);
				int ionIdx = stoi(terms[1]);
				std::string name = terms[2];
				ret.CircuitParam.push_back(CircuitDef{ idx, ionIdx, name });
				}
				break;
			case RM_IONS: {
				if (terms.size() != 9) { throw SapaException(std::format("Invalid ion definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				int nap = stoi(terms[1]);
				float nac = stof(terms[2]);
				int kp = stoi(terms[3]);
				float kc = stof(terms[4]);
				int cap = stoi(terms[5]);
				float cac = stof(terms[6]);
				int clp = stoi(terms[7]);
				float clc = stof(terms[8]);
				ret.IonParam.push_back(IonDef{ idx, nap, nac, kp, kc, cap, cac, clp, clc });
				}
				break;
			case RM_NETWORK:
				for (size_t i = 0; i < terms.size(); i += 3) {
					int sendIdx = stoi(terms[i]);
					float weight = stof(terms[i + 1]);
					int recIdx = stoi(terms[i+2]);
					ret.NetworkParam.push_back({sendIdx, weight, recIdx});
				}
				break;
			default:
				break;
		}
	}

	ifile.close();
	return ret;
}

std::string SAPACORE::File::Save(std::string path, SapaNetwork& network)
{
	return std::string();
}
