#pragma once

#include <string>
#include <vector>

namespace SAPA {
	
	struct Neuron;

	enum Trans {
		NONE = 0,
		EXC1 = 1,
		EXC2 = 2,
		INH1 = 4,
		INH2 = 8,
	};

	struct Dendrite {
		float weight;
		Neuron* target;
	};

	struct Neuron {
		std::vector<Dendrite> __dendrites;

		Neuron();

		bool receive();

	private:

		float charge;
		float bias;
		float decay;

		void activate();
	};

	class NRegion {
		std::vector<Neuron> __neurons;
	public:
		NRegion();
	};

	class NComplex {
		std::vector<NRegion> __regions;
	public:
		NComplex();
	};

}