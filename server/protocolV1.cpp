#include "protocolV1.h"
#include "methodPointer.h"
using namespace websocketpp::lib;
using namespace websocketpp::lib::placeholders;

map<server<config::asio> *, class protocolV1::world *> protocolV1::worlds;

protocolV1::protocolV1() : protocol(const_cast<char *>("v1")) {}

protocolV1::~protocolV1() {}

connection<config::asio>::message_handler protocolV1::getHandler(server<config::asio> *srv) {
    map<server<config::asio> *, class world *>::iterator it = worlds.find(srv);
    class world *wrld = it == worlds.end() ? createWorld(srv) : it->second;
    return bind(&world::handler, wrld, ::_1, ::_2);
}

class protocolV1::world *protocolV1::createWorld(server<config::asio> *server) {
    class world *wrld = new world();
    wrld->srv = server;
    return wrld;
}
