#include "pch.h"
#include "utils.h"

#include <cctype>
#include <algorithm>
#include <string>
#include <fstream>
#include <map>

namespace StringUtil {
	void toLower(std::string& value)
	{
		std::transform(value.begin(), value.end(), value.begin(), [](unsigned char c) {
			return std::tolower(c); });
	}

	void toUpper(std::string& value)
	{
		std::transform(value.begin(), value.end(), value.begin(), [](unsigned char c) {
			return std::toupper(c); });
	}

	std::vector<std::string> split(std::string& value, const char* delim)
	{
		std::size_t pre = 0, pos;
		std::vector<std::string> ret;
		bool quoted = false;

		while ((pos = value.find_first_of(delim, pre)) != std::string::npos) {
			if (pos > pre) { ret.push_back(value.substr(pre, pos - pre)); }
			pre = pos + 1;
		}

		if (pre < value.size()) { ret.push_back(value.substr(pre, std::string::npos)); }

		return ret;
	}

	std::string strip(std::string& value, const char* removals = " \t\r\n")
	{
		std::string ret;
		std::string enclStack;
		std::size_t len = strlen(removals);
		std::string remStr{ removals };

		for (size_t i = 0; i < value.size(); ++i) {
			char c = value[i];
			if (remStr.find(c) == std::string::npos) { ret += c; }
		}

		return ret;
	}
	
	bool isEmpty(std::string& value)
	{
		return (value.find_first_not_of(" \t\r\n") == std::string::npos);
	}
}

namespace FileUtil {

	struct PathInfo {
		fspath path;
		std::vector<std::string> __data;
		bool overwrite;

		PathInfo(fspath path, bool overwrite) {
			path = path;
			this->overwrite = overwrite;
		};

		PathInfo(){
			path = "";
			overwrite = false;
		}
	};
	
	std::map<std::string, PathInfo> pathdict;

	void writeToFile(fspath& dir, std::vector<std::string>& data, bool overwrite)
	{
		auto options = overwrite ?
			std::fstream::out | std::fstream::trunc :
			std::fstream::out | std::fstream::app;

		assurePath(dir);

		std::fstream fs;
		fs.open(dir, options);

		for (auto& v : dir) {
			fs << v << std::endl;
		}

		dir.clear();
		fs.close();
	}
	
	size_t vwriteFile(std::string& fileId, std::string& data)
	{
		if (!pathdict.contains(fileId)) { return -1; }
		pathdict[fileId].__data.push_back(data);
		return pathdict[fileId].__data.size();
	}

	size_t vwriteFile(std::string& fileId, std::vector<std::string>& data)
	{
		if (!pathdict.contains(fileId)) { return -1; }
		std::vector<std::string>* vals = &pathdict[fileId].__data;
		vals->insert(vals->end(), data.begin(), data.end());
		return vals->size();
	}

	void initVFile(std::string& fileId, fspath& path, bool overwrite)
	{
		if (pathdict.contains(fileId)) { return; }
		pathdict[fileId] = PathInfo{ path, overwrite };
	}

	void vflush(std::string& fileId)
	{
		if (!pathdict.contains(fileId)) { return; }
		auto& data = pathdict[fileId];

		writeToFile(data.path, data.__data, data.overwrite);
	}
	void vflush()
	{
		for(auto& entry : pathdict) {
			auto& info = entry.second;
			assurePath(info.path);
			writeToFile(info.path, info.__data, info.overwrite);
		}
	}
	void assurePath(fspath path)
	{
		if (!path.extension().empty()) {
			path = path.parent_path();
		}

		if (pathExists(path)) { return; }

		std::filesystem::create_directories(path);
	}

	bool readFile(fspath& path, std::vector<std::string>& data)
	{
		std::fstream fs;
		fs.open(path, std::fstream::in);
		if (fs.bad()) { return false; }
		else {
			std::string line;
			while (getline(fs, line)) { data.push_back(line); }
			fs.close();
		}

		return true;
	}
	bool pathExists(fspath& path)
	{
		return std::fstream(path).good();
	}

	fspath normalizePath(fspath newPath, fspath& relPath)
	{
		if (newPath.is_absolute()) { return newPath; }

		fspath ret = relPath / newPath;
		ret = ret.lexically_normal();

		return ret;
	}

	fspath execPath()
	{
#ifdef _WIN32
		wchar_t buff[MAX_PATH]{};
		GetModuleFileName(NULL, buff, MAX_PATH);
		std::wstring wstr{buff};
		fspath path{wstr.begin(), wstr.end()};
		path = path.parent_path();
#else

#endif
		return path;
	}
}

#pragma warning(disable: 6386)
void Algo::orderListByList(std::vector<std::string>& reference, std::vector<std::string>& values)
{
	std::vector<std::string> temp;
	for (auto& val : values) {
		auto itr = std::find(reference.begin(), reference.end(), val);
		if (itr != reference.end()) { temp.push_back(*itr); }
		else { temp.insert(temp.begin(), *itr); }
	}

	values = temp;
}
