#pragma once
#include <string>
#include "OS.hpp"
#include "commondef.hpp"

namespace SAPACORE {
	SAPICORE_API String GetEXEPath();
	SAPICORE_API String GetDir(String path);
	SAPICORE_API void AssurePath(String path);
	SAPICORE_API void MkDir(String path);
	SAPICORE_API std::string LoadFile(String path);
	SAPICORE_API void SaveFile(String path, std::string data);
	SAPICORE_API String GetFileName(String path);
	SAPICORE_API String GetExtension(String path);
}