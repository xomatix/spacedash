using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ClientExecute();
            Console.ReadLine();
        }

        static void ClientExecute()
        {
            string DISCONNECT_MSG = "!DISCONNECT";

            try {
         
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress item in ipHost.AddressList)
                {
                    Console.WriteLine(item);
                }
                IPAddress ipAddr = ipHost.AddressList[1];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
 
                // Creation TCP/IP Socket using
                // Socket Class Constructor
                Socket sender = new Socket(ipAddr.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
        
                try {
                    
                    // Connect Socket to the remote
                    sender.Connect(localEndPoint);
        
                    // We print EndPoint information
                    // that we are connected
                    Console.WriteLine("Socket connected to -> {0} ",
                                sender.RemoteEndPoint.ToString());

                    bool connected = true;
                    while (connected)
                    {
                        // Creation of message that
                        // we will send to Server
                        //byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                        Console.Write("> ");
                        byte[] messageSent = Encoding.ASCII.GetBytes(Console.ReadLine());
                        int byteSent = sender.Send(messageSent);
            
                        if (Encoding.UTF8.GetString(messageSent) == DISCONNECT_MSG)
                        {
                            connected = false;
                        }

                        // Data buffer
                        byte[] messageReceived = new byte[1024];
                        
            
                        // We receive the message using
                        // the method Receive(). This
                        // method returns number of bytes
                        // received, that we'll use to
                        // convert them to string
                        int byteRecv = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server -> {0}", Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
                        
                    }
        
        
                    // Close Socket using
                    // the method Close()
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    Console.WriteLine("[CLOSED CONNECTION] Closed conection with server");
                }
                
                // Manage of Socket's Exceptions
                catch (ArgumentNullException ane) {
                    
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                
                catch (SocketException se) {
                    
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                
                catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            
            catch (Exception e) {
                
                Console.WriteLine(e.ToString());
            }
        }
    }
}
