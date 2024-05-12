#pragma once

#include <string>
#include <vector>
#include <filesystem>
#include <map>

#include "error.h"
#include "symbols.h"

namespace SNDL {

	enum TokenErrorCode {
		//Closures
		MISMATCH_CLOSURE, MISSING_CLOSURE,
	};

	struct TokenError : ErrorBase {
		TokenErrorCode code;
		std::string Message();
		TokenError(std::filesystem::path& path, unsigned lineNo, std::string message, TokenErrorCode code);
	};

	struct Token {
		SymbolTypes type;
		std::string content;
		Token(SymbolTypes type, std::string content);
	};


	class TokenPage {

		std::filesystem::path __filePath;
		std::vector<SymbolTypes> __enclosures;
		std::vector<Token> __tokens;
		std::vector<TokenError> __errors;

		void addToken(std::string::iterator start, std::string::iterator end);
		bool addClosure(SymbolTypes closure);
		
		unsigned __lineNo;
	public:

		TokenPage(std::filesystem::path path);
		bool parse();
	};
}

