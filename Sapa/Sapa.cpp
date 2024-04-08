
#include <iostream>
#include <vector>

#include "ProcessManager.h"
#include "../SapaCore/SNCFileIO.h"
//#include "NrnStructures.hpp"
//#include "SNCFileIO.h"

//#include "../SapaCore/SNCFileIO.h"

//using namespace SAPACORE;
//using namespace SAPACORE::File;

int main()
{
    std::string prjPath = "G:\\Dev\\SapaCore\\CircuitDesigner\\bin\\Debug\\net8.0-windows\\Build\\LayeredNet.snc";
    auto params = SAPACORE::File::Load(prjPath);
    //SAPACORE::File NetworkSetupDetails params = SAPACORE::File::load(prjPath);
    //SAPACORE::File::NetworkSetupDetails params = SAPACORE::File::Load(prjPath);
    //SAPA::ProcessManager pMan;
}


