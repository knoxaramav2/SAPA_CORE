
#include <iostream>
#include <stdio.h>
#include <vector>

#include "ProcessManager.h"
#include "../SapaCore/SNCFileIO.h"
#include "ProcessManager.h"
#include "../SapaCore/FileUtil.h"

SAPACORE::SapaDiagnostic* __diag;

void IFnc(SAPACORE::InputAdapter* adapter) {
    static int i = 0;
    static int cycle = 0;
    //if (cycle++ % 3 != 0) { return; }

    bool vals[8]{
        KeyPressed(VK_UP),
        KeyPressed(VK_LEFT),
        KeyPressed(VK_RIGHT),
        KeyPressed(VK_DOWN),

        KeyPressed('W'),
        KeyPressed('A'),
        KeyPressed('D'),
        KeyPressed('S'),
    };

    /*for (size_t j = 0; j < adapter->Size(); ++j) {
        adapter->SetValue((j==i)*17.0f, j);
    }*/

    printf("\r\n\n\n\n\n\n");
    for (size_t j = 0; j < 8; ++j) {
        printf("%d | ", vals[j]);
        adapter->SetValue(vals[j] * 17.0f, j);
    }

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
    diag.SetSnapSlice(20);
    __diag = &diag;
    auto adapter = SAPACORE::NetworkIOAdapter(network, &IFnc, &OFnc);
    auto procman = SAPA::NetworkProcessManager(network, adapter, diag);
    printf("START\r\n");
    procman.Start();
    printf("STOP\r\n");

    diag.SaveCSV();
}

void test() {
    auto endTime = time(0) + (20 * 1);
    printf("\r\n\n\n");
    while (time(0) <= endTime) {
        bool press = KeyPressed('W');
        printf(">> %d\r", press);
    }
}

int main()
{
    //test();
    //return 0;
    std::string prjPath = "G:\\Dev\\SapaCore\\CircuitDesigner\\bin\\Debug\\net8.0-windows\\Build\\wasdarrow.snc";
    auto params = SAPACORE::File::Load(prjPath);
    SAPACORE::SapaNetwork network(
        params.InputParam, params.OutputParam, params.NeuronParam, params.NetworkParam,
        params.IonParam, params.CircuitParam);
    
    RunNetwork(network);
}


