#include "pch.h"
#include "tokens.h"

#include <fstream>
#include <sstream>

namespace SNDL {
	TokenPage::TokenPage(std::filesystem::path path)
	{
		__filePath = path;
		__lineNo = 0;
	}

	void TokenPage::addToken(std::string::iterator start, std::string::iterator end) {
		if (start >= end) { return; }
		std::string term{ start, end };

	}

	inline char reverseClosure(SymbolTypes closure) {
		switch (closure) {
		case '[': return ']';
		case ']': return '[';
		case '(': return ')';
		case ')': return '(';
		case '{': return '}';
		case '}': return '{';
		default: return ' ';
		}
	}

	bool TokenPage::addClosure(SymbolTypes closure)
	{
		std::string content;
		__tokens.push_back(Token{ closure, content });

		//End closure
		if (closure % 2 == 1) {
			if (__enclosures.empty()) {
				__errors.push_back(TokenError{ __filePath, __lineNo,
					std::format(""), MISSING_CLOSURE});
			}
		}

		return true;
	}

	bool TokenPage::parse()
	{
		std::ifstream fs;
		fs.open(__filePath, std::fstream::in);
		if (fs.bad()) { return false; }

		std::string line;
		while (getline(fs, line)) {
			++__lineNo;
			size_t lIdx = 0;
			for (size_t i = 0; i <= line.size(); ++i) {
				switch (line[i]) {
				case '{': addClosure(OP_BRACE); break;
				case '}': addClosure(CL_BRACE); break;
				case '(': addClosure(OP_PARAN); break;
				case ')': addClosure(CL_PARAN); break;
				case '[': addClosure(OP_BRACK); break;
				case ']': addClosure(CL_BRACK); break;
				case '\0':
					addToken(line.begin()+lIdx, line.end());
					break;
				}
			}
		}

		fs.close();

		return true;
	}
	Token::Token(SymbolTypes type, std::string content)
	{
		this->type = type;
		this->content = content;
	}
	std::string TokenError::Message()
	{
		std::filesystem::path fName = file->filename().replace_extension().string();
		//return std::format("{} TK{} {}: {}", fName, code, lineNo, msg);
		auto ret = std::format("{} TK{}", fName, (unsigned)code );
		return "";
	}
	TokenError::TokenError(
		std::filesystem::path& path, unsigned lineNo, 
		std::string message, TokenErrorCode code)
	{
		this->lineNo = lineNo;
		this->file = &path;
		this->msg = message;
		this->code = code;
	}
}
