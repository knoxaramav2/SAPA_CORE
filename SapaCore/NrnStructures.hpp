#pragma once

#include <string>
#include <vector>
#include <tuple>

#include "commondef.hpp"

namespace SAPACORE {

	typedef std::tuple<int, std::string, bool> InputDef;
	typedef std::tuple<int, std::string, bool> OutputDef;
	typedef std::tuple<int, std::string, float, float, float> NeuronDef;
	typedef std::tuple<int, float, int> CircuitDef;

	class QCell {
	protected:
		float __charge;
		float __bias;
		float __decayRate;//__decay illegal for some reason
		int __index;

	public:
		virtual void Update() = 0;
	};
	
	class Neuron: public QCell {
	public:
		Neuron(int index, float charge, float bias, float decay);
		void Update();
	};


	class IOCell: public QCell {
	protected:
		float __max;
		float __min;
	public:
		void Update();
	};

	class Input: public IOCell {
	public:
		Input(int index);
		void Excite(float value);
	};

	class Output: IOCell {
	public:
		Output(int index);
		float Retreive();
	};

	class SapaNetwork {
		Input** __inputs; size_t __numInputs;
		Output** __outputs; size_t __numOutputs;
		Neuron** __network; size_t __numNeurons;

		bool __running;
	public:
		
		SAPICORE_API SapaNetwork(
			std::vector<InputDef> inputs, 
			std::vector<OutputDef> outputs,
			std::vector<NeuronDef> neurons,
			std::vector<CircuitDef> connections);
		SAPICORE_API ~SapaNetwork();

		SAPICORE_API float GetOutput(size_t index);
		SAPICORE_API void SetInput(size_t index, float value);
		SAPICORE_API void Update();
		SAPICORE_API void Start();
		SAPICORE_API void Stop();
	};
}