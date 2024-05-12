#pragma once

#include "tokenPage.hpp"

namespace SNDL {
	class SndlPrecompiler {

		std::filesystem::path __srcPath;

	public:

		SndlPrecompiler(std::filesystem::path src);
		TokenPage* generateTokenPages();

	};
}
