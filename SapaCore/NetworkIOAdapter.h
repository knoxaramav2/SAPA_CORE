#pragma once
#include "NrnStructures.hpp"
namespace SAPACORE {

	struct InputAdapter;
	struct OutputAdapter;

	typedef void(*InputFnc)(InputAdapter&);
	typedef void(*OutputFnc)(OutputAdapter&);

	struct InputAdapter {
		SAPICORE_API InputAdapter(int size, InputFnc ifnc);
		SAPICORE_API ~InputAdapter();
		SAPICORE_API void SetValue(float value, int idx);
	private:
		int __size;
		float* __values;
		InputFnc __inputThread;
	};

	struct OutputAdapter {
		SAPICORE_API OutputAdapter(int size, OutputFnc ofnc);
		SAPICORE_API ~OutputAdapter();
		SAPICORE_API float GetValue(float value, int idx);
	private:
		int __size;
		float* __values;
		OutputFnc __outputThread;
	};

	class NetworkIOAdapter {
		SapaNetwork* __network;
	
		InputAdapter __inputs;
		OutputAdapter __outputs;
	public:
		SAPICORE_API NetworkIOAdapter(SapaNetwork& network, InputFnc ifnc, OutputFnc ofnc);
		SAPICORE_API InputAdapter* GetInputAdapter();
		SAPICORE_API OutputAdapter* GetOutputAdapter();
	};
}
