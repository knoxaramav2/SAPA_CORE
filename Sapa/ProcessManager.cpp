#include "ProcessManager.h"

#include <ctime>
#include <chrono>

void SAPA::NetworkProcessManager::__controlFnc()
{
	int i = 0;

	auto startTime = time(0);
	auto endTime = startTime + (60 * 1);

	while (__running) {
		//Input feed
		__iAdapter->Update();
		//Previous state signal cascade
		__neurons->StimuliUpdatePass();
		__neurons->DevPrint();

		//State adjustment
		__neurons->LocalUpdatePass();
		//Result collection
		__neurons->OutputUpdatePass();
		__oAdapter->Update();
		__neurons->DevPrint();
		++i;

		//printf("________________________________________\r\n");
		if (time(0)>=endTime) { Stop(); }
	}
}

SAPA::NetworkProcessManager::NetworkProcessManager(
	SAPACORE::SapaNetwork& network,
	SAPACORE::NetworkIOAdapter& adapter)
{
	__neurons = &network;
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
