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
}


