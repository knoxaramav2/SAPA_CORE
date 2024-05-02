#pragma once

#include <string>
#include <vector>
#include <filesystem>

#include "chemDefs.hpp"

namespace Config {

	enum CliOptions {
		NoInvoke = 0,
		CreateProject = 1,
		SaveSNC = 2,
		NoBuild = 4
	};

	class GlobalConfig {		
		GlobalConfig();

		std::filesystem::path __baseDir;
		unsigned long __utils = NoInvoke;

		bool ParseDD(std::string& com, std::string& val);
		bool ParseSD(std::string& com);

	public:
		std::filesystem::path __srcFile;
		std::filesystem::path __target;

		static GlobalConfig* GetInst();
		bool ProcessCli(int argc, char** argv);
		bool Validate();
		bool UtilRequested();
		bool CheckOptions(CliOptions option);

		std::filesystem::path GetSourcePath();
	};
}



