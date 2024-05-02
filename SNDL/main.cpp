#include "sndl_compiler.hpp"
#include "logger.hpp"
#include "config.hpp"
#include "string_utils.hpp"

#include <iostream>

static void compile() {
	Config::GlobalConfig* cfg = Config::GlobalConfig::GetInst();
	SNDL::SndlCompiler compiler;
	std::filesystem::path srcPath = cfg->GetSourcePath();
	std::string ext = srcPath.extension().string();
	StringUtils::toLower(ext);

	if (ext == ".sndl") {

	}


}

int main(int argc, char** argv) {

	Config::GlobalConfig* cfg = Config::GlobalConfig::GetInst();
	if (!cfg->ProcessCli(argc, argv) || !cfg->Validate()) {
		std::cout << "Exiting" << std::endl;
		Logger::FlushLog();
		return -1;
	}
	
	compile();

	Logger::FlushLog();

	return 0;
}