#include "pch.h"

#include "FileUtil.h"

#include <fstream>
#include <iostream>

String SAPACORE::GetEXEPath()
{
	String path;

#ifdef _WIN32
	TCHAR winpath[MAX_PATH];
	DWORD len = GetModuleFileName(NULL, winpath, MAX_PATH);
	path = winpath;
#elif __linux__
	throw Exception("Linux support on TODO list");
#endif

	return GetDir(path);
}

String SAPACORE::GetDir(String path)
{
	auto idx = path.find_last_of(L"/\\");
	if (idx == path.npos) { return L""; }
	return path.substr(0, idx);
}

void SAPACORE::AssurePath(String path) {
	
	if (GetExtension(path) != L"") {
		path = GetDir(path);
	}

	MkDir(path);
}

void SAPACORE::MkDir(String path)
{
	CreateDirectory(path.c_str(), NULL);
}

std::string SAPACORE::LoadFile(String path)
{
	throw std::exception("Not implemented");
}

void SAPACORE::SaveFile(String path, std::string data)
{
	std::ofstream ofile;
	ofile.open(path.c_str(), std::ios::binary);
	ofile << data;
	ofile.close();
}

String SAPACORE::GetFileName(String path)
{
	auto idx = path.find_last_of(L"\\/");
	if (idx == path.npos) { return path; }
	String fileName = path.substr(idx+1, path.size());
	return fileName;
}

String SAPACORE::GetExtension(String path)
{
	String file = GetFileName(path);
	auto idx = file.find_first_of(L'.');
	if (idx == file.npos) { return L""; }
	String ext = file.substr(idx, file.size());
	return ext;
}
