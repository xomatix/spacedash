

import socket


SIZE = 1024
FORMAT = "utf-8"
DISCONNECT_MSG = "!DISCONNECT"

def client():
    host = socket.gethostbyname(socket.gethostname())  # as both code is running on same pc
    port = 5000  # socket server port number

    client = socket.socket()  # instantiate
    client.connect((host, port))  # connect to the server

    print(f"[CONNECTED] Client connected to server at {host}:{port}")

    connected = True
    while connected:
        msg = input("> ")  # again take input

        client.send(msg.encode(FORMAT))

        if msg == DISCONNECT_MSG:
            connected = False
    
        else:
            msg = client.recv(SIZE).decode(FORMAT)
            print(f"[SERVER] {msg}")


    client.close()  # close the connection


if __name__ == '__main__':
    client()