#pragma once
#include "symbolTable.hpp"
#include "sndl_lexer.hpp"

namespace SNDL {

	class ParseTree {

	};

	class SndlParser {

		SymbolTable* __symTable;
		AST* __ast;

	public:
		SndlParser(SymbolTable* symTable);
		void Parse(TokenPage* page);

	};
}

