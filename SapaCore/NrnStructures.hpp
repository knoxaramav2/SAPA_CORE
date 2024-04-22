#pragma once

#include <string>
#include <vector>
#include <tuple>
#include <Bits.h>

#include "commondef.hpp"
#include "OS.hpp"

namespace SAPACORE {

	class SapaNetwork;
	class SapaDiagnostic;
	struct Dendrite;

	typedef std::tuple<int, std::string, bool, float> InputDef;
	typedef std::tuple<int, std::string, bool, float> OutputDef;
	typedef std::tuple<int, int, float, float, float, float, UINT64, UINT64> NeuronDef;
	typedef std::tuple<int, std::string, float, float, float, float, float, float, float, float>CircuitDef;
	typedef std::tuple<int, int, float, int, float, int, float, int, float> IonDef;
	typedef std::tuple<int, float, int> NetworkDef;

	struct CircuitConfig {
		const char* CircName;
		const float C_RST_M, C_RST_H, C_RST_N;
		const float C_REST, C_Vm;
		const float ENa, EK, EL;

		SAPICORE_API CircuitConfig(
			const char* name,
			float rstm,
			float rsth, float rstn,
			float rest, float vm,
			float ena, float ek, float el);

		SAPICORE_API CircuitConfig();
	};

	struct IonState {
		int NaParts;
		float NaConcentration;
		int KParts;
		float KConcenctration;
		int CaParts;
		float CaConcentration;
		int ClParts;
		float ClConcentration;

		IonState(
			int nap, float nac, int kp, float kc,
			int cap, float cac, int clp, float clc) {
			NaParts = nap;
			NaConcentration = nac;
			KParts = kp;
			KConcenctration = kc;
			CaParts = cap;
			CaConcentration = cac;
			ClParts = clp;
			ClConcentration = clc;
		}
	};

	class QCell {
	protected:
		//float __charge;
		float __threshold;
		//float __resistance;
		float __charge;//mV
		float __extCharge;//mV
		int __index;
		UINT64 __transCode;
		UINT32 __transmitter;
		std::vector<Dendrite> __dendrites;
		CircuitConfig* __circuitCfg;
		friend class SapaNetwork;
	public:
		virtual void UpdateLocalState() = 0;
		virtual void UpdateStimuliState() = 0;
		virtual void AddConnection(QCell* sender, float weight);
		virtual void PruneConnection(QCell* sender);
		virtual std::tuple<bool, UINT32> GetSignal();//TODO- Apply transmitter effects
		virtual float GetCharge();
	
		friend class SapaDiagnostic;
	};
	
	class Neuron: public QCell {
		float __rate_n;//Na+
		float __rate_h;//Na-
		float __rate_m;//K+

		float __Cm;//uF/cm^2
		float __GNa, __GK, __GL;//mS/cm^2

		bool __refactory;
		float __transmit;
		IonState* __intercell;
		IonState* __intracell;

		float calc_dndt(float v, float n);
		float calc_dhdt(float v, float h);
		float calc_dmdt(float v, float m);

	public:
		Neuron(int index, float cm,
			float gna, float gk, float gl,
			IonState* inter, IonState*intra,
			CircuitConfig*ccfg);
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
		virtual float GetCharge();
	};

	class Input: public IOCell {
	public:
		Input(int index, bool enabled);
		void Excite(float value);
		void AddConnection(QCell* sender, float weight);
		void PruneConnection(QCell* sender);
	};

	class Output: public IOCell {
	public:
		Output(int index, bool enabled);
		float Retreive();
	};

#define INRANGE(range, value) (value >= std::get<0>(range) && value <= std::get<1>(range))

	class SapaNetwork {
		Input** __inputs; size_t __numInputs; std::tuple<int, int> __inIdxRng;
		Output** __outputs; size_t __numOutputs; std::tuple<int, int> __outIdxRng;
		Neuron** __neurons; size_t __numNeurons; std::tuple<int, int> __netIdxRng;
		IonState** __ionStates; size_t __numIonStates;
		CircuitConfig** __circuitConfigs; size_t __numCircConfigs;

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
			std::vector<NetworkDef> connections,
			std::vector<IonDef> ions,
			std::vector<CircuitDef> circuits);
		SAPICORE_API ~SapaNetwork();

		SAPICORE_API float GetOutput(size_t index);
		SAPICORE_API void SetInput(size_t index, float value);
		SAPICORE_API void LocalUpdatePass();
		SAPICORE_API void StimuliUpdatePass();
		SAPICORE_API void OutputUpdatePass();

		friend class NetworkIOAdapter;
		friend class SapaDiagnostic;
	};

	//TODO Refactor out
	class SapaDiagnostic {
		SapaNetwork* __network;
		std::vector<std::vector<float>> __snapVals;
		String __diagDir;
		unsigned __snapSlice;
	public:
		SAPICORE_API SapaDiagnostic(SapaNetwork& network);
		SAPICORE_API void PrintActivity();
		SAPICORE_API void Snapshot();
		SAPICORE_API std::string GetCSV();
		SAPICORE_API void SaveCSV();
		SAPICORE_API void SaveCSV(String path);

		SAPICORE_API void SetSnapSlice(unsigned size);
	};
}