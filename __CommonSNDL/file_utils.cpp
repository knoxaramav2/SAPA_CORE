#include "pch.h"

#include "file_utils.hpp"
#include "string_utils.hpp"
#include <map>
#include <fstream>

std::map<std::string, std::vector<std::string>> __pathdict;

void normalize(std::string& raw) {
	StringUtils::toLower(raw);
}

std::string normalize(std::filesystem::path& raw) {
	std::string ret{raw.generic_string()};
	StringUtils::toLower(ret);
	return ret;
}

void FileUtils::writeToFile(std::filesystem::path dir, std::string& file, std::vector<std::string>& data, bool overwrite=false)
{
	assurePath(dir);
	dir /= file;
	std::string key = normalize(dir);

	int options = overwrite ? std::fstream::out | std::fstream::trunc : std::fstream::out | std::fstream::app;
	std::fstream ffile;
	ffile.open(key, options);

	for (auto& v : data) {
		ffile << v << std::endl;
	}

	ffile.close();
}

void FileUtils::writeToFile(const char* path, std::string& file, std::vector<std::string>& data, bool overwrite = false)
{
	std::filesystem::path dpath = std::filesystem::path{path};
	FileUtils::writeToFile(dpath, file, data, overwrite);
}

std::size_t FileUtils::vwriteFile(const char* path, std::vector<std::string>& data)
{
	std::string key = std::string{ path };
	normalize(key);

	if (__pathdict.find(key) == __pathdict.end()) {
		__pathdict[key] = std::vector<std::string>();
	}

	std::vector<std::string>* vals = &__pathdict[key];
	vals->insert(vals->end(), data.begin(), data.end());

	return vals->size();
}

std::size_t FileUtils::vwriteFile(const char* path, std::string& data)
{
	std::string key = std::string{ path };
	normalize(key);

	if (__pathdict.find(key) == __pathdict.end()) {
		__pathdict[key] = std::vector<std::string>();
	}

	std::vector<std::string>* vals = &__pathdict[key];
	vals->push_back(data);

	return vals->size();
}

void FileUtils::vflush(std::string& path)
{
	vflush(path.c_str());
}

void FileUtils::vflush(const char* path)
{
	std::string key{ path };
	normalize(key);
	assurePath(key);

	auto& data = __pathdict[key];

	std::fstream ffile;;
	ffile.open(key, std::fstream::out | std::fstream::app);

	for (auto& v : data) {
		ffile << v << std::endl;
	}

	data.clear();
	ffile.close();
}

bool FileUtils::makeFile(const char* path)
{
	return FileUtils::makeFile(std::string{path});
}

bool FileUtils::makeFile(std::string path)
{
	std::size_t lastIdx = path.find_last_of("/\\");
	if (lastIdx != std::string::npos) {
		std::string dir = path.substr(0, lastIdx);
		makeDir(dir);
	}

	std::fstream ffile;
	ffile.open(path, std::fstream::out);

	if (!ffile) {
		ffile.open(path, std::fstream::out | std::fstream::trunc);
		ffile << "";
	}
	else {
		return false;
	}
	
	ffile.close();

	return true;
}

void FileUtils::makeDir(const char* path)
{
	std::filesystem::create_directories(path);
}

void FileUtils::makeDir(std::string path)
{
	std::filesystem::create_directories(path);
}

bool FileUtils::fileExists(std::string path)
{
	return std::fstream(path).good();
}

bool FileUtils::assurePath(std::filesystem::path path)
{
	if (!path.extension().empty()) {
		path = path.parent_path();
	}

	return std::filesystem::create_directories(path);
}

std::filesystem::path FileUtils::normalizePathStr(std::string path, std::string defPath)
{
	return FileUtils::normalizePathStr(std::filesystem::path{path}, std::filesystem::path{ defPath });
}

std::filesystem::path FileUtils::normalizePathStr(std::filesystem::path path, std::filesystem::path defPath)
{
	if (path.is_relative()){
		return defPath / path;
	}

	return path;
}

std::filesystem::path FileUtils::joinPaths(std::string path1, std::string path2)
{
	std::filesystem::path p1{ path1 };
	std::filesystem::path p2{ path2 };
	return (p1 / p2);
}

std::filesystem::path FileUtils::joinPaths(std::filesystem::path path1, std::string path2)
{
	return path1 / std::filesystem::path{path2};
}

std::filesystem::path FileUtils::parentDir(std::string path)
{
	return std::filesystem::path{ path }.parent_path();
}

std::filesystem::path FileUtils::parentDir(const char* path)
{
	return std::filesystem::path{ path }.parent_path();
}

bool FileUtils::readFile(std::filesystem::path path, std::vector<std::string>& lines)
{
	std::fstream fs;
	fs.open(path, std::fstream::in);
	if (fs.bad()) { return false; }
	else {
		std::string line;
		while (getline(fs, line)) { lines.push_back(line); }
		fs.close();
	}
	
	return true;
}
