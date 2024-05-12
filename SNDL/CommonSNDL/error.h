#pragma once

#include <string>
#include <filesystem>

struct ErrorBase {
	unsigned lineNo;
	std::string msg;
	std::filesystem::path* file;

	virtual std::string Message() = 0;
};
