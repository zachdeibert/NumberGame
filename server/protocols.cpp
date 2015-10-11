#include "protocols.h"
#include "protocolV1.h"

class protocol *protocols[] = {
    new protocolV1(),
    NULL
};
