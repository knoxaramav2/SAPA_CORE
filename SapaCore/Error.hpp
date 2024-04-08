#pragma once

#include <string>

class SapaException : public std::exception {
	const char* msg;
public:
	SapaException() :msg() {};
	SapaException(const char* message) :msg(message) {};
	SapaException(std::string message) :msg(message.c_str()) {};

	const char* what() { return msg; }
};