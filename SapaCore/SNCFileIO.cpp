#include "pch.h"
#include "SNCFileIO.h"
#include "Error.hpp"

#include <format>
#include<fstream>

SAPACORE::File::NetworkSetupDetails SAPACORE::File::Load(std::string path)
{
	std::ifstream ifile;
	ifile.open(path);
	if (!ifile.is_open()) { throw SapaException(std::format("Unable to open project file at {}", path)); }

	return NetworkSetupDetails();
}

std::string SAPACORE::File::Save(std::string path, SapaNetwork& network)
{
	return std::string();
}
