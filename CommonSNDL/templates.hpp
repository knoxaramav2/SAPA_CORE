#pragma once

namespace SNDL {
	struct IonGx {
		//Ion concentration
		double INa, IK, IL;
	};

	struct Profile {
		unsigned int ID;
	};

	//Future impl.
	// Determine how to incorporate into HH calculation dynamically
	//struct Ion : Profile {
	//	int charge;
	//};

	struct Transmitter : Profile {
		
	};

	struct NeuronProfile : IonGx, Profile {

	};

	struct RegionProfile : IonGx, Profile {

	};

	struct InputProfile : Profile {
		float qCoeff;//cyclic charge multiplier
	};

	struct OutputProfile : Profile {
		float qCoeff;//cyclic charge multiplier
	};
}


