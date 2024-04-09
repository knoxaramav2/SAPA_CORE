#include "ProcessManager.h"

void SAPA::NetworkProcessManager::__controlFnc()
{
	int i = 0;
	while (__running) {
		//Input feed
		__iAdapter->Update();
		//Previous state signal cascade
		__network->StimuliUpdatePass();
		__network->DevPrint();

		//State adjustment
		__network->LocalUpdatePass();
		//Result collection
		__network->OutputUpdatePass();
		__oAdapter->Update();
		__network->DevPrint();
		++i;

		printf("________________________________________\r\n");
		if (i >= 10) { Stop(); }
	}
}

SAPA::NetworkProcessManager::NetworkProcessManager(
	SAPACORE::SapaNetwork& network,
	SAPACORE::NetworkIOAdapter& adapter)
{
	__network = &network;
	__adapter = &adapter;
	__iAdapter = adapter.GetInputAdapter();
	__oAdapter = adapter.GetOutputAdapter();
	__running = false;
}

SAPICORE_API void SAPA::NetworkProcessManager::Start()
{
	__running = true;
	__controlThread = std::thread(&NetworkProcessManager::__controlFnc, this);
	__controlThread.join();
}

SAPICORE_API void SAPA::NetworkProcessManager::Stop()
{
	__running = false;
}
