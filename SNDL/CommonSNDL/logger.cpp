#include "pch.h"
#include "Logger.h"
#include "Utils.h"

#include <stdio.h>
#include <ctime>
#include <sstream>

namespace Logger {
	std::vector<std::string> __logs;

	unsigned LOG_LIMIT = 50;
	std::filesystem::path LOG_PATH;
	std::string logFileId = "SNDLLOG";

	inline void InitLogPath() {
		if (LOG_PATH.empty()) { return; }
		std::stringstream ss;
		std::time_t t = std::time(nullptr);
		std::tm tm;
		localtime_s(&tm, &t);
		//ss << "logs/" << std::put_time(&tm, "%m_%d_%y") << ".log";
		//LOG_PATH = std::filesystem::path{ ss.str() };
		LOG_PATH = "logs/sndl.log";
		FileUtil::initVFile(logFileId, LOG_PATH, false);
	}

	inline const char* LogStr(LogType type) {
		return
			type == ERR ? "ERR" :
			type == WRN ? "WRN" :
			"INFO";
	}

	void log(LogType type, std::string msg, bool print)
	{
		std::string line = std::format("{}:: {}\r\n", LogStr(type), msg);
		if (print) { std::printf(line.c_str()); }
		__logs.push_back(line);

		if (__logs.size() >= LOG_LIMIT) { flushLog(); }
	}

	void flushLog()
	{
		InitLogPath();
		FileUtil::vflush(logFileId);
	}
}
