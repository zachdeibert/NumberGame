#include "protocolV1.h"

/*
  Client -> Server
    l: List games
    s[id]: Spectate game
    j[name]: Queue to join game
    e: Exit queue
    q[id]: Quit game
    t[row][changes]: Take turn
    d: Disconnect

  Server -> Client
    l[id],[id],[id],[...]: List of running games
    t[row][changes]: Turn taken by player
    j[id]: Game joined
    T[name]: Player [name]'s turn
    e[desc]: Error
 */

protocolV1::world::world() {}

protocolV1::world::~world() {}

void protocolV1::world::handler(connection_hdl hdl, server<config::asio>::message_ptr msg) {
    char *data = const_cast<char *>(msg->get_payload().c_str());
    switch ( *data ) {
        case 'l':
            listGames(hdl);
            break;
        case 's':
            spectateGame(hdl, data + 1);
            break;
        case 'j':
            queueJoining(hdl, data + 1);
            break;
        case 'e':
            exitQueue(hdl);
            break;
        case 'q':
            quitGame(hdl, data + 1);
            break;
        case 't':
            takeTurn(hdl, data + 1);
            break;
        case 'd':
            disconnect(hdl);
            break;
        default:
            unknownOpcode(hdl, *data, data + 1);
            break;
    }
}

void protocolV1::world::listGames(connection_hdl hdl) {}

void protocolV1::world::spectateGame(connection_hdl hdl, char *data) {}

void protocolV1::world::queueJoining(connection_hdl hdl, char *data) {}

void protocolV1::world::exitQueue(connection_hdl hdl) {}

void protocolV1::world::quitGame(connection_hdl hdl, char *data) {}

void protocolV1::world::takeTurn(connection_hdl hdl, char *data) {}

void protocolV1::world::disconnect(connection_hdl hdl) {}

void protocolV1::world::unknownOpcode(connection_hdl hdl, char opcode, char *data) {}
