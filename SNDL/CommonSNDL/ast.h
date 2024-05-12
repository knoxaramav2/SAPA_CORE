#pragma once
#include <cstdint>

#include "Tokens.h"

enum Lexeme {

};

struct AstNode {

	Lexeme lexeme;
	Token* content;
	AstNode** children;
	uint16_t numChild;
};


class AstTree {

public:

};