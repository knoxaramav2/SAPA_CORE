#include "sndl_compiler.hpp"

SNDL::SndlCompiler::SndlCompiler(): 
	__lexer(&__symTable), __parser(&__symTable)
{
	__cfg = Config::GlobalConfig::GetInst();
}

void SNDL::SndlCompiler::BuildAST()
{
	__lexer.BuildAst();
}

void SNDL::SndlCompiler::BuildToSNC()
{
}

std::string SNDL::SndlCompiler::BuildAsSNC()
{
	return std::string();
}


