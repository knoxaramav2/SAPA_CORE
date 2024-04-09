#include "pch.h"
#include "StringUtils.h"

std::vector<std::string> SAPACORE::StringUtils::strsplit(std::string string, std::string delim)
{
    delim += '\0';
    std::vector<std::string> terms;
    size_t tpos = 0;

    for (size_t i = 0; i <= string.length(); i++) {
        char cc = string[i];

        if (delim.find(string[i]) != delim.npos) {
            if (i-tpos>0) {
                std::string term = string.substr(tpos, (i - tpos)+(i+1==string.length()));
                term = strtrim(term);
                if (term.length() == 0) { continue; }
                terms.push_back(term);
            }
            tpos = i+1;
        }
    }

    if (tpos < string.length()) {
        terms.push_back(string.substr(tpos, string.size()));
    }

    return terms;
}

std::string SAPACORE::StringUtils::strtrim(std::string string)
{
    string.erase(string.begin(), std::find_if(string.begin(), string.end(), [](int c) { return !std::isspace(c); }));
    string.erase(std::find_if(string.rbegin(), string.rend(), [](unsigned char c) { return !std::isspace(c); }).base(), string.end());
    return string;
}
