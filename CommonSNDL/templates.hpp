#pragma once

namespace SNDL {
	//Ligand gate
	struct LGates {
		//Ion conductances
		double GNa, GK, GL;
	};

	struct Profile {
		unsigned int ID;
	};

	struct NeuronProfile : LGates, Profile {

	};

	struct RegionProfile : LGates, Profile {

	};

	struct InputProfile : Profile {

	};

	struct OutputProfile : Profile {

	};
}


