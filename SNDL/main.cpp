#include "sndl_compiler.hpp"
#include "logger.hpp"
#include "config.hpp"
#include "string_utils.hpp"
#include "projectUtils.hpp"

#include <iostream>

static void compile(Config::GlobalConfig* cfg) {
	if (cfg->CheckOptions(Config::CliOptions::NoBuild)) { return; }

	Config::GlobalConfig* cfg = Config::GlobalConfig::GetInst();
	SNDL::SndlCompiler compiler;
	std::filesystem::path srcPath = cfg->GetSourcePath();
	std::string ext = srcPath.extension().string();
	StringUtils::toLower(ext);

	if (ext == ".snc") {

	}
	else if (ext == ".sndl") {

	}
	else {
		Logger::Log(Logger::ERR, "Unrecognized input. Must specify .sndl or .snc file type.", true);
	}
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

