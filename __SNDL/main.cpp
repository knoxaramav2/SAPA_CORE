#include "sndl_compiler.hpp"
#include "logger.hpp"
#include "config.hpp"
#include "string_utils.hpp"
#include "projectUtils.hpp"
#include "sndl_precomp.h"

#include <iostream>

static bool compile(Config::GlobalConfig* cfg) {
	if (cfg->CheckOptions(Config::CliOptions::NoBuild)) { return; }

	Config::GlobalConfig* cfg = Config::GlobalConfig::GetInst();
	SNDL::SndlCompiler compiler;
	
	std::filesystem::path srcPath = cfg->GetSourcePath();
	std::string ext = srcPath.extension().string();
	StringUtils::toLower(ext);

	if (ext != ".sndl" && ext != ".snc") {
		Logger::Log(Logger::ERR, "Unrecognized input. Must specify .sndl or .snc file type.", true);
		return false;
	}

	if (ext == ".sndl") {
		SNDL::SndlPrecompiler precomp;
	}

	if (ext == ".snc") {
		SNDL::SncPrecompiler precomp;
	}

	if (!cfg->CheckOptions(Config::CliOptions::NoBuild)) {
		compiler.buildTarget("");
	}

	return true;
}

static void execPrecompCommands(Config::GlobalConfig* cfg) {
	if(cfg->CheckOptions(Config::CliOptions::CreateProject)){
		if (FileUtils::fileExists(cfg->GetSourcePath().string())) {
			Logger::Log(Logger::INFO, "Project exists. Skipping creation.", true);
		}
		else {
			ProjectUtils::CreateDefaultProject(cfg->GetSourcePath());
		}
	}
}

int main(int argc, char** argv) {

	Config::GlobalConfig* cfg = Config::GlobalConfig::GetInst();
	if (!cfg->ProcessCli(argc, argv) || !cfg->Validate()) {
		std::cout << "Exiting" << std::endl;
		Logger::FlushLog();
		return -1;
	}
	
	execPrecompCommands(cfg);
	compile(cfg);

	Logger::FlushLog();

	std::cout << "Done." << std::endl;

	return 0;
}

