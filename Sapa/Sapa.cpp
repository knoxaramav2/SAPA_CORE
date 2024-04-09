
#include <iostream>
#include <stdio.h>
#include <vector>

#include "ProcessManager.h"
#include "../SapaCore/SNCFileIO.h"
#include "ProcessManager.h"

SAPACORE::SapaDiagnostic* __diag;

void IFnc(SAPACORE::InputAdapter* adapter) {
    static int i = 0;
    static int cycle = 0;
    //if (cycle++ % 3 != 0) { return; }

    adapter->SetValue(1.1f, i);

    if (++i >= adapter->Size()) { i = 0; }
}

void OFnc(SAPACORE::OutputAdapter* adapter) {
    static int cycle = 0;
    //__diag->PrintActivity();
    /*printf("CYCLE(%d)_____________\r\n\t\t", cycle);
    for(int i = 0; i<adapter->Size(); ++i){ 
        printf("(%d)=%f | ",i, adapter->GetValue(i));
    }
    printf("\r\n");*/
    ++cycle;
}

void RunNetwork(SAPACORE::SapaNetwork& network) {
    auto diag = SAPACORE::SapaDiagnostic(network);
    __diag = &diag;
    auto adapter = SAPACORE::NetworkIOAdapter(network, &IFnc, &OFnc);
    auto procman = SAPA::NetworkProcessManager(network, adapter);
    printf("START\r\n");
    procman.Start();
    printf("STOP\r\n");
}

int main()
{
    std::string prjPath = "G:\\Dev\\SapaCore\\CircuitDesigner\\bin\\Debug\\net8.0-windows\\Build\\fibertest.snc";
    auto params = SAPACORE::File::Load(prjPath);
    SAPACORE::SapaNetwork network(params.InputParam, params.OutputParam, params.NeuronParam, params.CircuitParam);
    
    RunNetwork(network);
}


