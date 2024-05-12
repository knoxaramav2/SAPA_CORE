#pragma once
#include <map>
#include <string>
#include <vector>
#include <cstdint>
#include "types.hpp"

namespace SNDL {
	class SymbolTable {

		std::map<std::string, SymbolTable*> __scopes;
		std::vector<std::tuple<uint32_t, std::string>> __symbols;

	public:
		SymbolTable();

		bool RegisterScope(std::string scope, std::string symName);

	};
}

