#pragma once

#ifdef _WIN32
#include <Windows.h>

#define KeyPressed(c)(GetKeyState(c)&0x8000 ? true : false)

#elif __linux
throw Exception("Linux support on TODO list");
#endif

#ifndef UNICODE
typedef std::string String;
#else
typedef std::wstring String;
#endif