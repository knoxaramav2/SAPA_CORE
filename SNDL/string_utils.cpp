#include "string_utils.h"

#include <cctype>
#include <algorithm>

void toLower(std::string& value)
{
	std::transform(value.begin(), value.end(), value.begin(), [](unsigned char c) {
		return std::tolower(c); });
}

void toUpper(std::string& value)
{
	std::transform(value.begin(), value.end(), value.begin(), [](unsigned char c) {
		return std::toupper(c); });
}

std::vector<std::string> split(std::string value, const char* delim)
{
	std::size_t pre=0, pos;
	std::vector<std::string> ret;

	while ((pos = value.find_first_of(delim, pre)) != std::string::npos) {
		if (pos > pre) { ret.push_back(value.substr(pre, pos - pre)); }
		pre = pos + 1;
	}

	if (pre < value.size()) { ret.push_back(value.substr(pre, std::string::npos)); }

	return ret;
}
