import socket
import threading

SIZE = 1024
FORMAT = "utf-8"
DISCONNECT_MSG = "!DISCONNECT"

def handle_msg(msg):
    return f"msg recived: {msg}"

def handle_client(conn, addr):
    print(f"[NEW CONNECTION] {addr} connected")

    connected = True
    while connected:
        msg = conn.recv(SIZE).decode(FORMAT)

        if msg == DISCONNECT_MSG:
            connected = False

        print(f"[{addr}] {msg}")
        msg = handle_msg(msg)
        conn.send(msg.encode(FORMAT))
    conn.close()
    print(f"[CLOSED CONNECTION] Closed connection with {addr}")


def server():
    print("[STARTING] Server is starting")
    # get the hostname
    host = socket.gethostbyname(socket.gethostname())
    port = 5000  # initiate port no above 1024

    server = socket.socket()  # get instance
    server.bind((host, port))  # bind host address and port together
    server.listen()
    print(f"[LISTENING] Server is listening on {host}:{port}")

    while True:
        conn, addr = server.accept()  # accept new connection
        thread = threading.Thread(target=handle_client, args=(conn,addr))
        thread.start()
        print(f"[ACTIVE CONNECTIONS] {threading.active_count() - 1}")
        


if __name__ == '__main__':
    server()