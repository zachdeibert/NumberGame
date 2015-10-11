#ifndef PROTOCOL_H
#define PROTOCOL_H
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>
#include "methodPointer.h"
using namespace websocketpp;
using namespace websocketpp::lib;

class protocol {
    public:
        struct handlers {
            connection<config::asio>::message_handler message;
            close_handler close;
        };

        virtual ~protocol();
        virtual char *getVersion();
        virtual struct handlers getHandler(server<config::asio> *) = 0;

    protected:
        protocol(char *);

    private:
        char *version;
};

#endif
