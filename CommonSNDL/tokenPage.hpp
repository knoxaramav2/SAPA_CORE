#pragma once
#include <string>
#include <vector>

namespace SNDL {

	enum Lexeme {
		//Literals
		INT, DEC,
		CHAR, STRING,

		//Closures
		L_PARAN, R_PARAN,
		L_BRACK, R_BRACK,
		L_BRACE, R_BRACE,

		//Assignment
		SET,

		//Function
		
	};

	struct Token {
		std::string content;
		Lexeme type;
	};

	class TokenPage {
		std::vector<Token> __tokens;

	public:

		TokenPage();

	};
}