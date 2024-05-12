#include <vector>

#include "sndl_precompiler.hpp"
#include "file_utils.hpp"


SNDL::TokenPage* loadPage(std::filesystem::path path) {
	SNDL::TokenPage* page = new SNDL::TokenPage(path);
	return page;
}

SNDL::SndlPrecompiler::SndlPrecompiler(std::filesystem::path src)
{
	__srcPath = src;
}

SNDL::TokenPage* SNDL::SndlPrecompiler::generateTokenPages()
{
	TokenPage* basePage = loadPage(__srcPath);
	if (basePage == nullptr) { return nullptr; }

	return basePage;
}
