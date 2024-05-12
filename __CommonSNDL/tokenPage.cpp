#include <iostream>
#include <fstream>

#include "file_utils.hpp"
#include "tokenPage.hpp"

bool SNDL::TokenPage::hasPage(std::filesystem::path path)
{
	TokenPage* head = this->head();

	for (; head != nullptr; head = head->next()) {
		if (head->__pageLocalPath == path) {
			std::cout << "hasPage: " << head->__pageLocalPath << " == " << path << std::endl;
			return true;
		}
	}

	std::cout << "hasPage: NO MATCH " << path << std::endl;
	return false;
}

void SNDL::TokenPage::loadFromPath(std::filesystem::path path) {
	std::vector<std::string> lines;
	FileUtils::readFile(path, lines);


}

SNDL::TokenPage::TokenPage(std::filesystem::path path)
{
	__prev = nullptr;
	__next = nullptr;

	__basePath = path.parent_path();
	__pageLocalPath = path.filename();
}

void SNDL::TokenPage::append(TokenPage* target)
{
	TokenPage* tail = this->tail();

	tail->__next = target;
	target->__prev = tail;
}

SNDL::TokenPage* SNDL::TokenPage::head()
{
	TokenPage* crs = this;
	while (crs->__prev) { crs = crs->__prev; }
	return crs;
}

SNDL::TokenPage* SNDL::TokenPage::tail()
{
	TokenPage* crs = this;
	while (crs->__next) { crs = crs->__next; }
	return crs;
}

SNDL::TokenPage* SNDL::TokenPage::next() {
	return this->__next;
}

SNDL::TokenPage* SNDL::TokenPage::prev() {
	return this->__prev;
}



