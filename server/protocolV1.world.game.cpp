#include "protocolV1.h"

protocolV1::world::game::game(class player *players[3], int id) {
    this->players[0] = players[0];
    this->players[1] = players[1];
    this->players[2] = players[2];
    this->id = id;
}

protocolV1::world::game::~game() {}

bool protocolV1::world::game::isRunning() {
    return lost1 != NULL || lost2 != NULL;
}

int protocolV1::world::game::getId() {
    return id;
}

void protocolV1::world::game::addSpectator(class player *player) {
    spectators.push_back(player);
}

void protocolV1::world::game::removeSpectator(class player *player) {
    spectators.remove(player);
}

void protocolV1::world::game::playerDisconnected(class player *player) {
    removeSpectator(player);
    if ( players[0] == player || players[1] == player || players[2] == player ) {
        if ( lost1 != player && lost2 == NULL ) {
            lost2 = player;
        } else if ( lost1 == NULL && lost2 != player ) {
            lost1 = player;
        }
    }
}
