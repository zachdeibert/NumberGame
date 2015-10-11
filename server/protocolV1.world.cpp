#include <cmath>
#include "protocolV1.h"
using namespace std;
using namespace websocketpp::frame;
using namespace websocketpp::lib;
using namespace websocketpp::lib::placeholders;

/*
  Client -> Server
    l: List games
    s[id]: Spectate game
    j[name]: Queue to join game
    e: Exit queue
    q: Quit game
    t[row][changes]: Take turn
    d: Disconnect

  Server -> Client
    g[id],[id],[id],[...]: List of running games
    t[row][changes]: Turn taken by player
    j[id][name],[name],[name]: Game joined
    T[id][name]: Player's turn changed
    e[desc]: Error
    l[id][name]: Player temporary lost
    L[id][name]: Player permanently lost
    w[id][name]: Player un-lost
    W[id][name]: Player won
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
            quitGame(hdl);
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

void protocolV1::world::closeHandler(connection_hdl hdl) {
    disconnect(hdl);
}

class protocolV1::world::player *protocolV1::world::createPlayer(connection_hdl hdl, char *name) {
    struct sendData data;
    data.hdl = hdl;
    data.srv = srv;
    class player *user = new class player(bind(&world::sendFunc, data, ::_1), name);
    players.insert(pair<connection_hdl, class player *>(hdl, user));
    return user;
}

class protocolV1::world::game *protocolV1::world::createGame(class protocolV1::world::player *player) {
    class player *players[3];
    players[0] = queue[0];
    players[1] = queue[1];
    players[2] = player;
    for ( list<class game *>::iterator it = games.begin(); it != games.end(); it++ ) {
        if ( *it == NULL || !(*it)->isRunning() ) {
            return *it = new class game(players, distance(games.begin(), it));
        }
    }
    class game *game = new class game(players, games.size());
    games.push_back(game);
    return game;
}

void protocolV1::world::listGames(connection_hdl hdl) {
    int max = games.size();
    double maxLogExact = log10((double) max);
    int maxLog = (int) maxLogExact;
    if ( maxLog < maxLogExact ) {
        maxLog += 1;
    }
    int maxLen = (maxLog + 1) * max + 1;
    char *msg = new char[maxLen];
    bzero(msg, maxLen);
    msg[0] = 'g';
    int i = 1;
    bool first = true;
    for ( list<class game *>::iterator it = games.begin(); it != games.end(); it++ ) {
        if ( *it != NULL && (*it)->isRunning() ) {
            if ( first ) {
                first = false;
            } else {
                msg[i++] = ',';
            }
            sprintf(msg + i, "%d", (*it)->getId());
            while ( msg[i] != 0 ) {
                i++;
            }
        }
    }
    srv->send(hdl, msg, opcode::text);
}

void protocolV1::world::spectateGame(connection_hdl hdl, char *data) {
    srv->send(hdl, "ENot Implemented", opcode::text);
}

void protocolV1::world::queueJoining(connection_hdl hdl, char *data) {
    srv->send(hdl, "ENot Implemented", opcode::text);
}

void protocolV1::world::exitQueue(connection_hdl hdl) {
    srv->send(hdl, "ENot Implemented", opcode::text);
}

void protocolV1::world::quitGame(connection_hdl hdl) {
    srv->send(hdl, "ENot Implemented", opcode::text);
}

void protocolV1::world::takeTurn(connection_hdl hdl, char *data) {
    srv->send(hdl, "ENot Implemented", opcode::text);
}

void protocolV1::world::disconnect(connection_hdl hdl) {
    map<connection_hdl, class player *>::iterator player = players.find(hdl);
    if ( player != players.end() ) {
        for ( list<class game *>::iterator it = games.begin(); it != games.end(); it++ ) {
            (*it)->playerDisconnected(player->second);
        }
    }
}

void protocolV1::world::unknownOpcode(connection_hdl hdl, char opcode, char *data) {
    srv->send(hdl, "EUnknown opcode", opcode::text);
}

void protocolV1::world::sendFunc(struct protocolV1::world::sendData data, char *msg) {
    data.srv->send(data.hdl, msg, opcode::text);
}
