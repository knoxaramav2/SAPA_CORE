#include "config.h"
#include "utils.h"
#include "logger.h"

#include <iostream>
#include <map>

namespace SNDL {
	Config* __inst = nullptr;

	Config::Config()
	{
		__execDir = FileUtil::execPath();
		__relDir = std::filesystem::current_path();
		__srcPath = __relDir;
		__sncPath = __relDir;

		__targetLib = __execDir / "targets";
		__output = __execDir / "bin";

		__includes = {
			{__execDir / "stdlib"}
		};

		__wrnAsErr = false;
		__verbose = false;
	}

	inline static void missingArgErr(std::string& com){
		Logger::log(Logger::ERR, std::format("Missing required argument for '{}'", com), true);
	}

	inline static void illegaArgErr(std::string& com, std::string& val) {
		Logger::log(Logger::ERR, std::format("Illegal '{}' applied to '{}'", val, com), true);
	}

	bool Config::parseDD(std::string& com, std::string* val)
	{
		//Relational path options
		if (com == "--src") { 
			if (val == nullptr) { missingArgErr(com); return false; }
			else {
				__srcPath = FileUtil::normalizePath(fspath{ *val }, __relDir);
			}
		} else if ("--snc") {
			if (val == nullptr) { missingArgErr(com); return false; }
			else { 
				__sncPath = FileUtil::normalizePath(fspath{ *val }, __relDir); 
			}
		}
		else if ("--out") {
			if (val == nullptr) { missingArgErr(com); return false; }
			else {
				__sncPath = FileUtil::normalizePath(fspath{ *val }, __relDir);
			}
		}

		//Exec dir bound options
		else if ("--target") {
			if (val == nullptr) { missingArgErr(com); return false; }
			else {
				__targetLib = FileUtil::normalizePath(fspath{ *val }, __targetLib);
			}
		}
		else if ("--include") {
			if (val == nullptr) { illegaArgErr(com, *val); return false; }
			else {
				__includes.push_back(FileUtil::normalizePath(*val, __relDir));
			}
		}

		else if (com == "--verbose") {
			if (val == nullptr) { missingArgErr(com); return false; }
			else {
				__verbose = true;
			} 
		} else {
			Logger::log(Logger::ERR, std::format("Unrecognized option '{}'", com), true);
			return false;
		}

		return true;
	}

	bool Config::parseSD(char com)
	{
		switch (com) {
		case 'W': __wrnAsErr = true; break;
		default: 
			Logger::log(Logger::ERR, std::format("Unrecognized option '{}'", com), true);
			return false;
		}

		return true;
	}

	bool Config::ParseCli(char** argv, int argc)
	{
		bool succ = true;
		std::map<std::string, uint8_t> appliedOptions = {
			{"--src", 0},
			{"--snc", 0},
			{"--verbose", 0},
			{"-W", 0},
			{"--out", 0},
			{"--target", 0},
			{"--include", -1},
		};

		for (int i = 1; i < argc; ++i) {
			std::string line = argv[i];
			auto trms = StringUtil::split(line, " \t\r\n=");
			if (!appliedOptions.contains(trms[0]) || appliedOptions[trms[0]] == 1) {
				auto errMsg = !appliedOptions.contains(trms[0]) ? "Unrecognized" : "Duplicate";
				Logger::log(Logger::ERR, std::format("{} option '{}'", errMsg, trms[0]), true);
				succ = false;
				continue;
			}
			else {
				appliedOptions[trms[0]] |= 1;
				succ &= trms[0][1] == '-' ? 
					parseDD(trms[0], trms.size() == 1 ? nullptr : &trms[1]): 
					parseSD(trms[0][1]);
			}
		}

		Logger::flushLog();
		return succ;
	}

	Config* Config::Instance()
	{
		if (__inst == nullptr) { __inst = new Config(); }
		return __inst;
	}

}

