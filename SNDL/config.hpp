#pragma once

class GlobalConfig {
	GlobalConfig();

public:
	static GlobalConfig* GetInst();
	bool ProcessCli(int argc, char**argv);

};

