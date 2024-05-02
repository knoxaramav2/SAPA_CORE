#pragma once

#include <string>
#include <vector>
#include <filesystem>

#include "chemDefs.hpp"

namespace Config {

	class GlobalConfig {		
		GlobalConfig();

		std::filesystem::path __baseDir;
		std::filesystem::path __srcFile;
		std::filesystem::path __target;
		

		bool ParseDD(std::string& com, std::string& val);
		bool ParseSD(std::string& com);

	public:
		static GlobalConfig* GetInst();
		bool ProcessCli(int argc, char** argv);
		bool Validate();

		std::filesystem::path GetSourcePath();
	};
}



