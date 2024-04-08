#pragma once

#include <string>
#include <vector>
#include <tuple>

#include "commondef.hpp"
#include <Bits.h>

namespace SAPACORE {

	class SapaNetwork;

	typedef std::tuple<int, std::string, bool> InputDef;
	typedef std::tuple<int, std::string, bool> OutputDef;
	typedef std::tuple<int, std::string, float, float, float> NeuronDef;
	typedef std::tuple<int, float, int> CircuitDef;

	struct Dendrite;
	class QCell {
	protected:
		float __charge;
		float __bias;
		float __decayRate;//__decay illegal for some reason
		int __index;
		UINT32 __transmitter;
		std::vector<Dendrite> __dendrites;
		friend class SapaNetwork;
	public:
		virtual void UpdateLocalState() = 0;
		virtual void UpdateStimuliState() = 0;
		void AddConnection(QCell* sender, float weight);
		void PruneConnection(QCell* sender);
		std::tuple<bool, UINT32> GetSignal();//TODO- Apply transmitter effects
	};
	
	class Neuron: public QCell {
	public:
		Neuron(int index, float charge, float bias, float decay);
		void UpdateLocalState();
		void UpdateStimuliState();
	};

	struct Dendrite {
	private:
		
	public:
		QCell* sender;
		float weight;
		
		Dendrite(QCell* sender, float w){
			this->sender = sender;
			weight = w;
		}
	};

	class IOCell: public QCell {
	protected:
		float __max;
		float __min;
	public:
		void UpdateLocalState();
		void UpdateStimuliState();
	};

	class Input: public IOCell {
	public:
		Input(int index);
		void Excite(float value);
	};

	class Output: public IOCell {
	public:
		Output(int index);
		float Retreive();
	};

#define INRANGE(range, value) (value >= std::get<0>(range) && value <= std::get<1>(range))

	class SapaNetwork {
		Input** __inputs; size_t __numInputs; std::tuple<int, int> __inIdxRng;
		Output** __outputs; size_t __numOutputs; std::tuple<int, int> __outIdxRng;
		Neuron** __network; size_t __numNeurons; std::tuple<int, int> __netIdxRng;
		std::vector<Dendrite> __dendrites;

		Neuron* __findNeuronByIdx(int idx);
		Input* __findInputByIdx(int idx);
		Output* __findOutputByIdx(int idx);
		QCell* __findByIdx(int idx);
	public:
		
		SAPICORE_API SapaNetwork(
			std::vector<InputDef> inputs, 
			std::vector<OutputDef> outputs,
			std::vector<NeuronDef> neurons,
			std::vector<CircuitDef> connections);
		SAPICORE_API ~SapaNetwork();

		SAPICORE_API float GetOutput(size_t index);
		SAPICORE_API void SetInput(size_t index, float value);
		SAPICORE_API void LocalUpdatePass();
		SAPICORE_API void StimuliUpdatePass();
		SAPICORE_API void InputUpdatePass();
		SAPICORE_API void OutputUpdatePass();

		friend class NetworkIOAdapter;
	};
}