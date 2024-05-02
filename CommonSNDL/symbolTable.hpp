#pragma once
#include <map>
#include <string>
#include "types.hpp"

namespace SNDL {
	class SymbolTable {

		std::map<std::string, SymbolTable*> __scopes;

	public:
		SymbolTable();

		bool RegisterScope(std::string scope, std::string symName);

	};
}

