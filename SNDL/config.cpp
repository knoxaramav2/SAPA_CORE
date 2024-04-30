#include "config.hpp"
#include "logger.hpp"
#include "string_utils.h"

GlobalConfig* __inst = nullptr;

GlobalConfig::GlobalConfig()
{
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
	for (int i = 1; i < argc; ++i) {
		std::string raw{argv[i]};


	}

	return false;
}
