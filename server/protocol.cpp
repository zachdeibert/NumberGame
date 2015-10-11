#include <cstring>
#include "protocol.h"

protocol::protocol(char *version) {
    this->version = version;
}

protocol::~protocol() {}

char *protocol::getVersion() {
    int len = strlen(this->version) + 1;
    char *version = new char[len];
    bcopy(this->version, version, len);
    return version;
}
