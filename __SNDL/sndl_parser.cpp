#include "sndl_parser.hpp"

SNDL::SndlParser::SndlParser(SymbolTable* symTable, TokenPage* page)
{
	__symTable = symTable;
	__tPage = page;
}