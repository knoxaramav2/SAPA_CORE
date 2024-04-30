#include "logger.hpp"
#include "file_utils.hpp"

#include <vector>
#include <filesystem>
#include <map>
#include <iostream>
#include <sstream>
#include <time.h>
#include <chrono>

#include "file_utils.hpp"

const char* LogTypeStr[] = { "WRN", "ERR", "INFO" };
const char* LogPath = nullptr;

const int MAX_STACK_SIZE = 50;

inline static void InitLogPath() {
	if (LogPath == nullptr) {
		std::stringstream ss;
		std::time_t t = std::time(nullptr);
		std::tm tm;
		localtime_s(&tm, &t);

		ss << std::put_time(&tm, "%g") << ".log";
		FileUtils::makeFile(ss.str());
	}
}

void Log(LogType type, const char* msg, bool print)
{
	Log(type, std::string(msg), print);
}

void Log(LogType type, std::string msg, bool print)
{
	InitLogPath();
	std::string nstr = std::format("{}:: {}", LogTypeStr[type], msg);

	if (print) {
		std::cout << nstr << std::endl;
	}

	if (FileUtils::vwriteFile(LogPath, nstr) >= MAX_STACK_SIZE) { FlushLog(); }
}

void FlushLog()
{
	InitLogPath();
	FileUtils::vflush(LogPath);
}
