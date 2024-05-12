#pragma once

#include <string>
#include <vector>
#include <filesystem>

namespace SNDL{
	class Config {
	
		std::filesystem::path __execDir;
		std::filesystem::path __srcPath;
		std::filesystem::path __relDir;
		std::filesystem::path __sncPath;
		std::filesystem::path __targetLib;
		std::filesystem::path __output;
		std::vector<std::filesystem::path> __includes;

		bool __wrnAsErr;
		bool __verbose;

		Config();

		bool parseDD(std::string& com, std::string* val);
		bool parseSD(char com);

	public:

		bool ParseCli(char** argv, int argc);
		static Config* Instance();
	};
}


