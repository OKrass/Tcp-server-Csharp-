using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpListenerApp
{
    class Program
    {
        //Configuring port
        const int port = 3001; 
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                // Type IP adress here
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                // Starting listeners
                server.Start();

                while (true)
                {
                    Console.WriteLine("Awaiting for listeners... ");

                    // Accepting client
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Listener connected...");

                    // Getting stream 
                    NetworkStream stream = client.GetStream();

                    // Information which is goin to be sent to the user
                    string message = "why so serious";
                    // Transforming data into into string of bytes
                    byte[] data = Encoding.UTF8.GetBytes(message);

                    // Sending message
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Data sent: {0}", message);
                    // Closing connection
                    client.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}