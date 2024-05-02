#pragma once
#include <filesystem>

class ChemDef {

	//Max Conductances
	double GNa;
	double GK;
	double GL;

public:

	ChemDef(double gna, double gk, double gl);
	static ChemDef Load(std::filesystem::path path);

};