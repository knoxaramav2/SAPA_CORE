#include "SNDLexer.h"
#include "config.h"

#include <iostream>

int main(int argc, char**argv)
{
	SNDL::Config* config = SNDL::Config::Instance();
	if (!config->ParseCli(argv, argc)) {
		return -1;
	}

	return 0;
}


