using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            TcpListener server = null;

            try
            {
                // Set the TcpListener on port 13000.
                const int port = 13000;
                var localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                var bytes = new byte[256];

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine($@"Waiting for a connection... {Environment.NewLine}");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    var client = server.AcceptTcpClient();
                    Console.WriteLine($@"Connected!");

                    // Get a stream object for reading and writing
                    var stream = client.GetStream();
                    //client.SendTimeout = 1000;
                    //client.ReceiveTimeout = 1000;
                    stream.ReadTimeout = 1000;
                    stream.WriteTimeout = 1000;

                    var sendData = new StringBuilder();

                    try
                    {
                        // Loop to receive all the data sent by the client.
                        int i;
                        Console.WriteLine($"Recieved:");
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            var data = Encoding.ASCII.GetString(bytes, 0, i);
                            Console.Write(data);
                            sendData.Append(data);
                        }

                        var msg = Encoding.ASCII.GetBytes(sendData.ToString());
                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine($"{Environment.NewLine}Sent:{Environment.NewLine}{sendData}");
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode != SocketError.TimedOut) throw;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null) throw;
                        if (((SocketException)ex.InnerException).SocketErrorCode != SocketError.TimedOut) throw;
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                // Stop listening for new clients.
                server?.Stop();
            }
        }
    }
}
