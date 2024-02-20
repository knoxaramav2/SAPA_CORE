
#include <iostream>
#include <vector>
#include "sapi.hpp"
#include "Prototypes.hpp"

SAPI::Blueprint CreateBlueprint() {
    SAPI::Blueprint bp("base");

    

    return bp;
}

int main()
{
    SAPI::Init();
    SAPI::Blueprint bp = CreateBlueprint();
    SAPI::BuildFromBlueprint(bp);
}


