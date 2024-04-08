
#include <iostream>
#include <vector>

#include "ProcessManager.h"
#include "../SapaCore/SNCFileIO.h"
#include "ProcessManager.h"

void IFnc(SAPACORE::InputAdapter& adapter) {

}

void OFnc(SAPACORE::OutputAdapter& adapter) {

}

void RunNetwork(SAPACORE::SapaNetwork& network) {
    auto adapter = SAPACORE::NetworkIOAdapter(network, &IFnc, &OFnc);
    auto iAdapter = adapter.GetInputAdapter();
    auto oAdapter = adapter.GetOutputAdapter();
    auto procman = SAPA::ProcessManager(adapter);
}

int main()
{
    std::string prjPath = "G:\\Dev\\SapaCore\\CircuitDesigner\\bin\\Debug\\net8.0-windows\\Build\\LayeredNet.snc";
    auto params = SAPACORE::File::Load(prjPath);
    SAPACORE::SapaNetwork network(params.InputParam, params.OutputParam, params.NeuronParam, params.CircuitParam);
    
    RunNetwork(network);
}


