#pragma once

#include <string>
#include <vector>
#include <tuple>

#include "commondef.hpp"
#include <Bits.h>

namespace SAPACORE {

	class SapaNetwork;
	class SapaDiagnostic;
	struct Dendrite;

	typedef std::tuple<int, std::string, bool, float> InputDef;
	typedef std::tuple<int, std::string, bool, float> OutputDef;
	typedef std::tuple<int, std::string, float, float, float, UINT64, bool> NeuronDef;
	typedef std::tuple<int, float, int> CircuitDef;

	class QCell {
	protected:
		float __charge;
		float __bias;
		float __decayRate;//__decay illegal for some reason
		int __index;
		UINT64 __transCode;
		UINT32 __transmitter;
		std::vector<Dendrite> __dendrites;
		friend class SapaNetwork;
	public:
		virtual void UpdateLocalState() = 0;
		virtual void UpdateStimuliState() = 0;
		virtual void AddConnection(QCell* sender, float weight);
		virtual void PruneConnection(QCell* sender);
		virtual std::tuple<bool, UINT32> GetSignal();//TODO- Apply transmitter effects
	
		friend class SapaDiagnostic;
	};
	
	class Neuron: public QCell {
		bool __refactory;
		float __hCharge;
	public:
		Neuron(int index, float charge, float bias, float decay, UINT64 transcode, bool refactory);
		void UpdateLocalState();
		void UpdateStimuliState();
		std::tuple<bool, UINT32> GetSignal();
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

		float GetCharge();
	};

	class IOCell: public QCell {
	protected:
		float __max;
		float __min;
		bool __enabled;
	public:
		void UpdateLocalState();
		void UpdateStimuliState();
	};

	class Input: public IOCell {
	public:
		Input(int index, bool enabled, float decay);
		void Excite(float value);
		void AddConnection(QCell* sender, float weight);
		void PruneConnection(QCell* sender);
	};

	class Output: public IOCell {
	public:
		Output(int index, bool enabled, float decay);
		float Retreive();
	};

#define INRANGE(range, value) (value >= std::get<0>(range) && value <= std::get<1>(range))

	class SapaNetwork {
		Input** __inputs; size_t __numInputs; std::tuple<int, int> __inIdxRng;
		Output** __outputs; size_t __numOutputs; std::tuple<int, int> __outIdxRng;
		Neuron** __neurons; size_t __numNeurons; std::tuple<int, int> __netIdxRng;
		//std::vector<Dendrite*> __dendrites;

		Neuron* __findNeuronByIdx(int idx);
		Input* __findInputByIdx(int idx);
		Output* __findOutputByIdx(int idx);
		QCell* __findByIdx(int idx);
	public:
		//DEV-- Remove
		SAPICORE_API void DevPrint();
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
		SAPICORE_API void OutputUpdatePass();

		friend class NetworkIOAdapter;
		friend class SapaDiagnostic;
	};

	class SapaDiagnostic {
		SapaNetwork* __network;
	public:
		SAPICORE_API SapaDiagnostic(SapaNetwork& network);
		SAPICORE_API void PrintActivity();
	};
}