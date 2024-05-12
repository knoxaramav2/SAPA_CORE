#include "projectUtils.hpp"
#include "file_utils.hpp"

bool ProjectUtils::CreateDefaultProject(std::filesystem::path path)
{
	std::filesystem::path dir;
	std::string fileName;

	if (path.extension().empty()) {
		fileName = path.filename().string()+".sndl";
		dir = path;
	}
	else {
		fileName = path.filename().string();
		dir = path.parent_path();
	}

	std::string data = 
		"#Default SNDL project\n\n"
		"#Initialize current system\n"
		"use(std)\n"
		"ns(\"root\")\n"
		"inputs(10)\n"
		"outputs(10)\r\n"
		"#Define structure profiles\n\n"
		"#Design code\n"
		"#Attach each input to each of neurons 0 - 9\n"
		"$input[1...10] => $neuron[1...10]\n"
		"$neuron[1...10] -> $neuron[11] -> $outputs[1...10]\n"
		;

	std::vector<std::string> vdat{ data };

	FileUtils::writeToFile(dir, fileName, vdat, true);

	return true;
}