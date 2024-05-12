#pragma once

#include "symbolTable.hpp"
#include "sndl_lexer.hpp"
#include "sndl_parser.hpp"
#include "config.hpp"

#include <string>

namespace SNDL {

	enum Stage {
		UTIL, SNDL, SNC
	};

	class SndlCompiler {

		Config::GlobalConfig* __cfg;
		SymbolTable __symTable;
		SndlLexer __lexer;
		SndlParser __parser;

		Stage __stage = UTIL;

	public:

		SndlCompiler();

		//SNDL STAGE
		void BuildAST(std::filesystem::path srcPath);
		void BuildToSNC();
		
		//SNC STAGE
		std::string BuildAsSNC();


		//TARGET STAGE
		void buildTarget(std::string target);
	};
}