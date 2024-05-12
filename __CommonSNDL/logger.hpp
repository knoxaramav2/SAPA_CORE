#pragma once
#include "utils.hpp"
#include <string>

namespace Logger {
	enum LogType {
		WRN, ERR, INFO
	};

	void Log(LogType type, const char* msg, bool print);
	void Log(LogType type, std::string msg, bool print);
	void FlushLog();
}



