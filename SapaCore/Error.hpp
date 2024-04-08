#pragma once

#include <string>

class SapaException : public std::exception {
	const char* msg;
public:
	SapaException() :msg() {};
	SapaException(const char* message) :msg(message) {};

	const char* what() { return msg; }
};