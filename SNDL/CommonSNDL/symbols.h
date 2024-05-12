#pragma once

#include <map>
#include <string>

enum SymbolTypes {
	//CLOSURES
	OP_PARAN, CL_PARAN,
	OP_BRACE, CL_BRACE,
	OP_BRACK, CL_BRACK,

	//LITERALS
	SYMBOL, STRING,
	INT, DECI,

	//SYM MODIFIERS
	DOT, SYM_ID, PROJECT,

	//OPERATORS
	ADD, SUB, MULT, DIV, MOD, POW,

	//Unary
	INVERT,

	//Assignment
	SET_NS, SET_IN_RANGE, SET_OUT_RANGE,
	RANGE, SPECIFY, AS,

	//Misc
	BSLASH, FSLASH, AMP,

	//Util
	S_COMMENT, M_COMMENT_S, M_COMMENT_E,

	//Connectors

};

std::map<SymbolTypes, const char*> SymMap = {
	//Closures
	{OP_PARAN, "("}, {CL_PARAN, ")"},
	{OP_BRACE, "{"}, {CL_BRACE, "}"},
	{OP_BRACK, "["}, {CL_BRACK, "]"},
	//Sym Modifiers
	{DOT, "."}, {SYM_ID, "$"}, {PROJECT, "->"},
	//Arith
	{ADD, "+"},{SUB, "-"},{MULT, "*"},{DIV, "/"},{MOD, "%"}, {POW, "^"},
	//Unary
	{INVERT, "!"},
	//Assignment
	{SET_NS, "ns"}, {SET_IN_RANGE, "inputs"}, {SET_OUT_RANGE, "outputs"},
	{RANGE, "..."}, {SPECIFY, "specify"}, {AS, "as"},
	//Misc
	{BSLASH, "\\"}, {FSLASH, "/"}, {AMP, "@"},
	//Util
	{S_COMMENT, "#"}, {M_COMMENT_S, "#*"}, {M_COMMENT_E, "*#"}

 };