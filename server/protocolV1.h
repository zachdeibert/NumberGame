#ifndef PROTOCOLV1_H
#define PROTOCOLV1_H
#include <list>
#include <map>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>
#include "protocol.h"
using namespace std;
using namespace websocketpp;
using namespace websocketpp::lib;

class protocolV1 : public protocol {
    public:
        class world {
            friend class protocolV1;

            public:
                world();
                virtual ~world();

            protected:
                class game;

                class player {
                    public:
                        player(function<void(char *)>, char *);
                        virtual ~player();
                        virtual void send(char *);
                        virtual char *getName();

                    protected:
                        char *name;
                        function<void(char *)> sendFunc;
                        list<class game *> spectating;
                        class game *playing;
                };
                class game {
                    public:
                        game(class player *[3], int);
                        virtual ~game();
                        virtual bool isRunning();
                        virtual int getId();
                        virtual void addSpectator(class player *);
                        virtual void removeSpectator(class player *);
                        virtual void playerDisconnected(class player *);

                    protected:
                        int id;
                        list<class player *> spectators;
                        class player *players[3];
                        class player *lost1;
                        class player *lost2;
                };

                server<config::asio> *srv;
                class player *queue[2];
                map<connection_hdl, class player *> players;
                list<class game *> games;

                virtual void handler(connection_hdl, server<config::asio>::message_ptr);
                virtual void closeHandler(connection_hdl);
                virtual class player *createPlayer(connection_hdl, char *);
                virtual class game *createGame(class player *);
                virtual void listGames(connection_hdl);
                virtual void spectateGame(connection_hdl, char *);
                virtual void queueJoining(connection_hdl, char *);
                virtual void exitQueue(connection_hdl hdl);
                virtual void quitGame(connection_hdl);
                virtual void takeTurn(connection_hdl, char *);
                virtual void disconnect(connection_hdl);
                virtual void unknownOpcode(connection_hdl, char, char *);

            private:
                struct sendData {
                    connection_hdl hdl;
                    server<config::asio> *srv;
                };
                static void sendFunc(struct sendData, char *);
        };

        protocolV1();
        virtual ~protocolV1();
        virtual struct handlers getHandler(server<config::asio> *);

    protected:
        virtual class world *createWorld(server<config::asio> *);

    private:
        static map<server<config::asio> *, class world *> worlds;
};

#endif
