#pragma once
#include <string>
#include <map>

#include "templates.hpp"

namespace SNDL {
	enum ValType {
		INT, FLOAT, CHAR, STRING
	};

	enum TempType {
		NEURON, REGION, INPUT, OUTPUT
	};

	struct SndlTemplate {
		std::string name;
		TempType type;
	};

	struct SndlAlias {
		std::string name;
		std::string value;
		ValType type;
	};

	class SndlTypeTable {

		std::map<std::string, SndlTemplate> __types;
		std::map<std::string, std::string> __alias;

	public:

		SndlTypeTable();
		bool RegisterType(std::string name, SndlTemplate type);
		bool RegisterAlias(std::string name, std::string value);

	};
}


