#include "pch.h"

#include "component_utils.hpp"

SNDL::CompositeID::CompositeID(std::string path)
{
	__fullId = path;
}

void SNDL::CompositeID::setPath(std::string path)
{
	__fullId = path;
}

void SNDL::CompositeID::append(std::string path)
{
	if (__fullId.size() == 0) { __fullId = path; }
	else { __fullId += "." + path; }
}

bool SNDL::CompositeID::pop()
{
	size_t idx = __fullId.find_first_of('.');
	if (idx == 0) { return false; }
	__fullId.erase(__fullId.begin(), __fullId.begin() + idx);
	return true;
}
