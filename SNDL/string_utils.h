#pragma once
#include <string>
#include <vector>

namespace StringUtils {
	void toLower(std::string& value);
	void toUpper(std::string& value);

	std::vector<std::string> split(std::string& value, const char* delim);
	std::string strip(std::string& value, const char* removals);
}


