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
char* LogPath = nullptr;

const int MAX_STACK_SIZE = 50;

inline static void InitLogPath() {
	if (LogPath == nullptr) {
		std::stringstream ss;
		std::time_t t = std::time(nullptr);
		std::tm tm;
		localtime_s(&tm, &t);
		std::string str{ ss.str() };
		LogPath = new char[str.length()];
		memcpy(LogPath, str.c_str(), str.length()+1);
		FileUtils::makeFile(ss.str());
	}
}

void Logger::Log(Logger::LogType type, const char* msg, bool print)
{
	Logger::Log(type, std::string(msg), print);
}

void Logger::Log(Logger::LogType type, std::string msg, bool print)
{
	InitLogPath();
	std::string nstr = std::format("{}:: {}", LogTypeStr[type], msg);

	if (print) {
		std::cout << nstr << std::endl;
	}

	if (FileUtils::vwriteFile(LogPath, nstr) >= MAX_STACK_SIZE) { FlushLog(); }
}

void Logger::FlushLog()
{
	InitLogPath();
	FileUtils::vflush(LogPath);
}
