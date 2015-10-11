#ifndef PROTOCOLV1_H
#define PROTOCOLV1_H
#include <map>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>
#include "protocol.h"
#include "methodPointable.h"
using namespace std;
using namespace websocketpp;

class protocolV1 : public protocol {
    public:
        class world : private methodPointable {
            friend class protocolV1;

            public:
                world();
                virtual ~world();

            protected:
                virtual void handler(connection_hdl, server<config::asio>::message_ptr);
                virtual void listGames(connection_hdl);
                virtual void spectateGame(connection_hdl, char *);
                virtual void queueJoining(connection_hdl, char *);
                virtual void exitQueue(connection_hdl hdl);
                virtual void quitGame(connection_hdl, char *);
                virtual void takeTurn(connection_hdl, char *);
                virtual void disconnect(connection_hdl);
                virtual void unknownOpcode(connection_hdl, char, char *);

            private:
                server<config::asio> *srv;
        };

        protocolV1();
        virtual ~protocolV1();
        virtual connection<config::asio>::message_handler getHandler(server<config::asio> *);

    protected:
        virtual class world *createWorld(server<config::asio> *);

    private:
        static map<server<config::asio> *, class world *> worlds;
};

#endif
