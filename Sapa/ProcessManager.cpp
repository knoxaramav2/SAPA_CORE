#include "ProcessManager.h"

void SAPA::ProcessManager::__controlThread()
{
	while (__running) {
		
	}
}

SAPA::ProcessManager::ProcessManager(SAPACORE::NetworkIOAdapter& adapter)
{
	__adapter = &adapter;
	__iAdapter = adapter.GetInputAdapter();
	__oAdapter = adapter.GetOutputAdapter();

	__running = false;
}

SAPICORE_API void SAPA::ProcessManager::Start()
{
	__running = true;
}

SAPICORE_API void SAPA::ProcessManager::Stop()
{
	__running = false;
}
