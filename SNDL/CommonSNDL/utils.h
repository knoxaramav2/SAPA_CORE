#pragma once

#include <string>
#include <vector>
#include <filesystem>

#ifdef _WIN32
#include <Windows.h>
#else

#endif

namespace fs = std::filesystem;
typedef fs::path fspath;

namespace StringUtil {
	void toLower(std::string& value);
	void toUpper(std::string& value);

	std::vector<std::string> split(std::string& value, const char* delim);
	std::string strip(std::string& value, const char* removals);
	bool isEmpty(std::string& value);
}

namespace FileUtil {
	void writeToFile(fspath& path, std::vector<std::string>& data, bool overwrite);

	size_t vwriteFile(std::string& fileId, std::string& data);
	size_t vwriteFile(std::string& fileId, std::vector<std::string> & data);
	void initVFile(std::string& fileId, fspath& path, bool overwrite);

	void vflush(std::string& fileId);
	void vflush();

	void assurePath(fspath path);
	bool readFile(fspath& path, std::vector<std::string>& data);
	bool pathExists(fspath& path);

	fspath normalizePath(fspath newPath, fspath& relPath);

	fspath execPath();
}

namespace Algo {

	void orderListByList(std::vector<std::string>& reference, std::vector<std::string>& values);

}