#pragma once

#include "symbolTable.hpp"
#include "tokenPage.hpp"
#include "component_utils.hpp"

#include <vector>
#include <string>

namespace SNDL {

	enum TokenTypes {

	};


	struct Token {

	};

	class AST {

	public:

	};

	class SrcPage {

	public:
		CompositeID Id;
	};
	
	class SndlLexer {

		SymbolTable* __symTable;

	public:

		SndlLexer(SymbolTable* symTable);
		void BuildAst(SrcPage page);

	};
}
