#include "sndl.h"
#include "logger.hpp"
#include "config.hpp"

#include <iostream>

int main(int argc, char** argv) {

	Config::GlobalConfig* cfg = Config::GlobalConfig::GetInst();
	if (!cfg->ProcessCli(argc, argv) || !cfg->Validate()) {
		std::cout << "Exiting" << std::endl;
		Logger::FlushLog();
		return -1;
	}
	

	
	Logger::FlushLog();

	return 0;
}