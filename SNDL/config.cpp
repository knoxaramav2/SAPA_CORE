#include "config.hpp"
#include "logger.hpp"
#include "string_utils.hpp"
#include "os_iop.hpp"

#include <iostream>

using namespace Config;

GlobalConfig* __inst = nullptr;

GlobalConfig::GlobalConfig()
{
	__baseDir = std::filesystem::current_path();
}

bool GlobalConfig::ParseDD(std::string& com, std::string& val)
{
	if (com == "src"){
		__srcFile = FileUtils::normalizePathStr(std::filesystem::path{ val },
			FileUtils::joinPaths(__baseDir, "projects"));
	}
	else if (com == "target") {
		__target = FileUtils::normalizePathStr(std::filesystem::path{ val },
			FileUtils::joinPaths(GetExecPath(), "targets"));
	}
	else {
		Logger::Log(Logger::LogType::ERR, std::format("Unknown command: {}", com), true);
		return false;
	}

	return true;
}

bool GlobalConfig::ParseSD(std::string& com)
{
	if (com == "snc") { __utils |= SaveSNC; }
	else if (com == "nobuild") { __utils |= NoBuild; }
	else if (com == "create") { __utils |= CreateProject; }
	else {
		Logger::Log(Logger::LogType::ERR, std::format("Unknown command: {}", com), true);
		return false;
	}

	return true;
}

GlobalConfig* GlobalConfig::GetInst()
{
	if (__inst == nullptr) {
		__inst = new GlobalConfig();
	}

	return __inst;
}

bool GlobalConfig::ProcessCli(int argc, char** argv)
{
	bool succ = true;

	__baseDir = FileUtils::parentDir(argv[0]);

	for (int i = 1; i < argc; ++i) {
		std::string raw{argv[i]};
		std::vector<std::string> vals = StringUtils::split(raw, "=");
		std::string com, val;

		if (vals.size() > 2) {
			Logger::Log(Logger::ERR, std::format("Multiple assignment not allowed: {}", raw), true);
			succ = false;
			continue;
		}

		com = vals[0];
		StringUtils::toLower(com);

		if (vals.size() == 2) {
			val = vals[1];
			ParseDD(com, val);
		}
		else {
			ParseSD(com);
		}
	}

	return succ;
}

bool Config::GlobalConfig::Validate()
{
	bool succ = true;
	
	if (!Config::GlobalConfig::CheckOptions(Config::CreateProject) && !std::filesystem::exists(__srcFile)) {
		Logger::Log(Logger::ERR, std::format("Source file not found: {}", __srcFile.string()), true);
		succ = false;
	}

	if (__target.empty()) {
		if (!SaveSNC) {
			Logger::Log(Logger::INFO, std::format("No output defined. Performing analysis anyway."), true);
		}
		else {
			__utils |= NoBuild;
		}
		
	} else if (!std::filesystem::exists(__target)) {
		Logger::Log(Logger::ERR, std::format("Target generator not found: {}", __target.string()), true);
		succ = false;
	}

	return succ;
}

bool Config::GlobalConfig::UtilRequested()
{
	return !__utils;
}

bool Config::GlobalConfig::CheckOptions(CliOptions option)
{
	return __utils & option;
}

std::filesystem::path Config::GlobalConfig::GetSourcePath() { return __srcFile; }



