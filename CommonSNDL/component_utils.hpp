#pragma once
#include <string>
#include <vector>

namespace SNDL {

	struct CompositeID {

		CompositeID(std::string path);
		void setPath(std::string path);
		void append(std::string path);
		bool pop();

	private:
		std::string __fullId;
	};

}