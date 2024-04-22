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
				if (terms.size() != 8) { throw SapaException(std::format("Invalid input definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				int ccidx = stoi(terms[1]);
				float cm = stof(terms[2]);
				float gna = stof(terms[3]);
				float gk = stof(terms[4]);
				float gl = stof(terms[5]);
				int ioni = stoi(terms[6]);
				int ione = stoi(terms[7]);

				NeuronDef value = { idx, ccidx, cm, gna, gk, gl, ioni, ione };
			}
			break;
			case RM_CIRCUIT: {
				if (terms.size() != 10) { throw SapaException(std::format("Invalid ion definition: line {}", lineno)); }
				int idx = stoi(terms[0]);
				std::string name = terms[1];
				float rst_m = stof(terms[2]);
				float rst_h = stof(terms[3]);
				float rst_n = stof(terms[4]);
				float rest = stof(terms[5]);
				float vm = stof(terms[6]);
				float ena = stof(terms[7]);
				float ek = stof(terms[8]);
				float el = stof(terms[9]);

				ret.CircuitParam.push_back(CircuitDef{ idx, name, rst_m, rst_h, rst_n, rest, vm, ena, ek, el });
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
