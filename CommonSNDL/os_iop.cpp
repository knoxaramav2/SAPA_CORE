#include "pch.h"

#include "os_iop.hpp"
#include <stdexcept>
#include <filesystem>


#ifdef _WIN32
#include <windows.h>

std::filesystem::path GetExecPath()
{
	wchar_t buff[MAX_PATH]{};
	GetModuleFileName(NULL, buff, MAX_PATH);
	std::wstring wstr(buff);
	std::filesystem::path ret(wstr.begin(), wstr.end());
	ret = ret.parent_path();
	return ret;
}

#else

std::string GetExecPath() {
	throw std::exception("GetExecPath not implemented (Linux)");
}

#endif

