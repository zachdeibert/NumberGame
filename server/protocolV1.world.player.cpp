#include <cstring>
#include "protocolV1.h"
using namespace websocketpp::lib;

protocolV1::world::player::player(function<void(char *)> sendFunc, char *name) {
    this->sendFunc = sendFunc;
    int namelen = strlen(name);
    this->name = new char[namelen];
    bcopy(name, this->name, namelen);
}

protocolV1::world::player::~player() {
    delete name;
}

void protocolV1::world::player::send(char *msg) {
    sendFunc(msg);
}

char *protocolV1::world::player::getName() {
    int len = strlen(name);
    char *dup = new char[len];
    bcopy(name, dup, len);
    return dup;
}
