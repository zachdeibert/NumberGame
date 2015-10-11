#ifndef PROTOCOL_H
#define PROTOCOL_H
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>
#include "methodPointer.h"
using namespace websocketpp;
using namespace websocketpp::lib;

class protocol {
    public:
        typedef methodPointerDuo<connection_hdl, server<config::asio>::message_ptr> handler;
        virtual ~protocol();
        virtual char *getVersion();
        virtual connection<config::asio>::message_handler getHandler(server<config::asio> *) = 0;

    protected:
        protocol(char *);

    private:
        char *version;
};

#endif
