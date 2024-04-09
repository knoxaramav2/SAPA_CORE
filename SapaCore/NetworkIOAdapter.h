#pragma once
#include "NrnStructures.hpp"
namespace SAPACORE {

	struct InputAdapter;
	struct OutputAdapter;

	typedef void(*InputFnc)(InputAdapter*);
	typedef void(*OutputFnc)(OutputAdapter*);

	struct InputAdapter {
		SAPICORE_API InputAdapter(int size, InputFnc ifnc, Input**inputs);
		SAPICORE_API ~InputAdapter();
		SAPICORE_API void SetValue(float value, int idx);
		SAPICORE_API void Update();
		SAPICORE_API int Size();
	private:
		int __size;
		InputFnc __inputFunc;
		Input** __inputs;
	};

	struct OutputAdapter {
		SAPICORE_API OutputAdapter(int size, OutputFnc ofnc, Output**outputs);
		SAPICORE_API ~OutputAdapter();
		SAPICORE_API float GetValue(int idx);
		SAPICORE_API void Update();
		SAPICORE_API int Size();
	private:
		int __size;
		OutputFnc __outputFunc;
		Output** __outputs;
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
