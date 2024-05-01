#pragma once
#include "os_iop.hpp"
#include <string>
#include <fstream>
#include <filesystem>

namespace FileUtils {
	void writeToFile(std::filesystem::path dir, std::string& file, std::vector<std::string>& data, bool overwrite);
	void writeToFile(const char* path, std::string& file, std::vector<std::string>& data, bool overwrite);
	std::size_t vwriteFile(const char* path, std::vector<std::string>& data);
	std::size_t vwriteFile(const char* path, std::string& data);
	void vflush(std::string& path);
	void vflush(const char* path);
	void makeFile(const char* path);
	void makeFile(std::string path);
	void makeDir(const char* path);
	void makeDir(std::string path);
	bool fileExists(std::string path);
	std::filesystem::path normalizePathStr(std::string path, std::string defPath);
	std::filesystem::path normalizePathStr(std::filesystem::path path, std::filesystem::path defPath);
	std::filesystem::path joinPaths(std::string path1, std::string path2);
	std::filesystem::path joinPaths(std::filesystem::path path1, std::string path2);
	std::filesystem::path parentDir(std::string path);
	std::filesystem::path parentDir(const char* path);
}


