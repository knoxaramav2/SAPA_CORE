#pragma once
#include "../SapaCore/NetworkIOAdapter.h"

#include <thread>

namespace SAPA {
	class NetworkProcessManager {
		SAPACORE::SapaNetwork* __neurons;
		SAPACORE::NetworkIOAdapter* __adapter;
		SAPACORE::InputAdapter* __iAdapter;
		SAPACORE::OutputAdapter* __oAdapter;
		bool __running;
		void __controlFnc();
		std::thread __controlThread;
	public:
		SAPICORE_API NetworkProcessManager(
			SAPACORE::SapaNetwork& network,
			SAPACORE::NetworkIOAdapter& adapter);
		SAPICORE_API void Start();
		SAPICORE_API void Stop();
	};
}

