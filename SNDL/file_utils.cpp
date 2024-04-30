#include "file_utils.hpp"
#include "string_utils.h"
#include <map>
#include <fstream>

std::map<std::string, std::vector<std::string>> __pathdict;

void normalize(std::string& raw) {
	toLower(raw);
}

std::string normalize(std::filesystem::path& raw) {
	std::string ret{raw.generic_string()};
	toLower(ret);
	return ret;
}

void FileUtils::writeToFile(std::filesystem::path dir, std::string& file, std::vector<std::string>& data, bool overwrite=false)
{
	dir /= file;
	std::string key = normalize(dir);

	int options = overwrite ? std::fstream::out | std::fstream::app : std::fstream::out | std::fstream::trunc;
	std::fstream ffile;
	ffile.open(key, options, options);

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

	auto& data = __pathdict[key];

	std::fstream ffile;
	ffile.open(key, std::fstream::out | std::fstream::app);

	for (auto& v : data) {
		ffile << v << std::endl;
	}

	data.clear();
	ffile.close();
}

void FileUtils::makeFile(const char* path)
{
	FileUtils::makeFile(std::string{path});
}

void FileUtils::makeFile(std::string path)
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

	ffile.close();
}

void FileUtils::makeDir(const char* path)
{
	std::filesystem::create_directories(path);
}

void FileUtils::makeDir(std::string path)
{
	std::filesystem::create_directories(path);
}
