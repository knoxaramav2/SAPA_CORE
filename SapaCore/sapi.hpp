#pragma once

#ifdef SAPICORE_EXPORTS
#define SAPICORE_API __declspec(dllexport)
#else
#define SAPICORE_API __declspec(dllimport)
#endif

/*
Exposed API
Intended to be used in language wrappers and direct use
*/

namespace SAPI {
	extern "C" SAPICORE_API void test();
}