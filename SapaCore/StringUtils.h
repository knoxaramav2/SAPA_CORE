#pragma once
#include <string>
#include <vector>

#include "commondef.hpp"

namespace SAPACORE::StringUtils {

	extern SAPICORE_API std::vector<std::string> strsplit(std::string string, std::string delim);
	extern SAPICORE_API std::string strtrim(std::string string);
}