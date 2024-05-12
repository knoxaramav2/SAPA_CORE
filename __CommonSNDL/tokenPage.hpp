#pragma once
#include <string>
#include <vector>
#include <filesystem>

namespace SNDL {

	enum Lexeme {
		//Symbols
		AMPERSAND, OCTOTHRORPE, BANG,
		DOLLAR, MOD, CARET, BACKTICK,
		STAR, 

		//Closures
		OP_PARAN, CL_PARAN,
		OP_BRACK, CL_BRACK,
		OP_BRACE, CL_BRACE,
		SQUOTE, DQUOTE,

		//Ops
		MIN, ADD, FSLASH, BSLASH,
		COLON, SEMI, TILDE, QUESTION,
		COMMA, POINT, LESS, GRTR, AT,
		UNDERSCORE, PIPE,

		//AlphaNum
		INT, DECI, SYMBOL,

		//Whitespace
		NEWLINE, TAB
	};

	struct Token {
		std::string content;
		Lexeme type;

		Token(Lexeme type, std::string content);
	};

	class TokenPage {
		std::vector<Token> __tokens;

		static std::filesystem::path __basePath;
		std::filesystem::path __pageLocalPath;

		TokenPage* __prev;
		TokenPage* __next;

		bool hasPage(std::filesystem::path path);
		void loadFromPath(std::filesystem::path path);

	public:

		TokenPage(std::filesystem::path path);

		void append(TokenPage* target);

		TokenPage* next();
		TokenPage* prev();

		TokenPage* head();
		TokenPage* tail();

	};
}