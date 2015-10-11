#include <iostream>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>
#include "protocol.h"
#include "protocols.h"
#include "methodPointer.h"
using namespace std;
using namespace websocketpp;
using namespace websocketpp::frame;
using namespace websocketpp::lib;
using namespace websocketpp::lib::placeholders;

server<config::asio> *srv;

void handleMessage(connection_hdl, server<config::asio>::message_ptr);

int main() {
    srv = new server<config::asio>();
    srv->set_message_handler(handleMessage);
    srv->init_asio();
    srv->listen(8080);
    srv->start_accept();
    srv->run();
    return 0;
}

void handleMessage(connection_hdl hdl, server<config::asio>::message_ptr msg) {
    char *version = const_cast<char *>(msg->get_payload().c_str());
    for ( int i = 0; protocols[i] != NULL; i++ ) {
        char *proto = protocols[i]->getVersion();
        int diff = strcmp(version, proto);
        delete proto;
        if ( diff == 0 ) {
            // Switch protocols
            protocol::handlers handlers = protocols[i]->getHandler(srv);
            endpoint<connection<config::asio>, config::asio>::connection_ptr conn = srv->get_con_from_hdl(hdl);
            conn->set_message_handler(handlers.message);
            conn->set_close_handler(handlers.close);
            srv->send(hdl, "S", opcode::text);
            return;
        }
    }
    // Invalid protocol
    srv->send(hdl, "E", opcode::text);
}
