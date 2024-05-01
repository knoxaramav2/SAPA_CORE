#include "chemDefs.h"
#include "logger.hpp"
#include "utils.hpp"
#include <fstream>
#include <string>

ChemDef::ChemDef(double gna, double gk, double gl)
{
	GNa = gna;
	GK = gk;
	GL = gl;
}

ChemDef ChemDef::Load(std::filesystem::path path)
{
	//default conductances
	double gna = 1.2, gk = 0.36, gl = 0.003;

	std::vector<std::string> lines;
	if (FileUtils::readFile(path, lines)) {
		for (std::string line : lines) {
			if (StringUtils::isEmpty(line)) { continue; }
			auto trms = StringUtils::split(line, " \t");
			if (trms.size() != 2) {
				Logger::Log(Logger::LogType::WRN, std::format("Invalid chemical definition: {}", line), true);
				continue;
			}
			StringUtils::toUpper(trms[0]);
			if (trms[0] == "COND_NA") { gna = stof(trms[1]); }
			else if (trms[0] == "COND_K") { gk = stof(trms[1]); }
			else if (trms[0] == "COND_L") { gl = stof(trms[1]); }
		}
	}
	else {
		Logger::Log(Logger::LogType::WRN, "Chemical definition file not found. Resorting to defaults.", true);
	}

	return ChemDef(gna, gk, gl);
}
