#pragma once
#include "../SapaCore/NetworkIOAdapter.h"

#include <thread>

namespace SAPA {
	class ProcessManager {
		SAPACORE::NetworkIOAdapter* __adapter;
		SAPACORE::InputAdapter* __iAdapter;
		SAPACORE::OutputAdapter* __oAdapter;
		bool __running;
		void __controlThread();
	public:
		SAPICORE_API ProcessManager(SAPACORE::NetworkIOAdapter& adapter);
		SAPICORE_API void Start();
		SAPICORE_API void Stop();
	};
}

