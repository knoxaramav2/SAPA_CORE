#pragma once

#include <string>
#include <filesystem>

namespace Logger {

	enum LogType {
		ERR, WRN, INFO
	};

	void log(LogType type, std::string msg, bool print);
	void flushLog();

}
