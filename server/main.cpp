#include <iostream>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>
using namespace std;
using namespace websocketpp;

void onMessage(connection_hdl hdl, server<config::asio>::message_ptr msg) {
    cout << msg->get_payload() << endl;
}

int main() {
    server<config::asio> server;
    server.set_message_handler(onMessage);
    server.init_asio();
    server.listen(8080);
    server.start_accept();
    server.run();
    return 0;
}
