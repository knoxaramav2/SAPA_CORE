#include "pch.h"
#include "string_utils.hpp"

#include <cctype>
#include <algorithm>

void StringUtils::toLower(std::string& value)
{
	std::transform(value.begin(), value.end(), value.begin(), [](unsigned char c) {
		return std::tolower(c); });
}

void StringUtils::toUpper(std::string& value)
{
	std::transform(value.begin(), value.end(), value.begin(), [](unsigned char c) {
		return std::toupper(c); });
}

std::vector<std::string> StringUtils::split(std::string& value, const char* delim)
{
	std::size_t pre=0, pos;
	std::vector<std::string> ret;
	bool quoted = false;

	while ((pos = value.find_first_of(delim, pre)) != std::string::npos) {
		if (pos > pre) { ret.push_back(value.substr(pre, pos - pre)); }
		pre = pos + 1;
	}

	if (pre < value.size()) { ret.push_back(value.substr(pre, std::string::npos)); }

	return ret;
}

std::string StringUtils::strip(std::string& value, const char* removals=" \t\r\n")
{
	std::string ret;
	std::string enclStack;
	std::size_t len = strlen(removals);
	std::string remStr{removals};

	for (size_t i = 0; i < value.size(); ++i) {
		char c = value[i];
		if (remStr.find(c) == std::string::npos) { ret += c; }
	}

	return ret;
}

bool StringUtils::isEmpty(std::string value)
{
	return (value.find_first_not_of(" \t\r\n") == std::string::npos);
}
