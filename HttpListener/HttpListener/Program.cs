using System;
using System.Text;
using System.Net;
using System.Threading;

namespace HttpListener
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var listener = new System.Net.HttpListener();

            listener.Prefixes.Add("http://localhost:8080/");
            listener.Prefixes.Add("http://127.0.0.1:8080/");

            listener.Start();

            while (true)
            {
                try
                {
                    var context = listener.GetContext(); //Block until a connection comes in
                    context.Response.StatusCode = 200;
                    context.Response.SendChunked = true;

                    var bytes = Encoding.UTF8.GetBytes("Hue Hue Hue\n");
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    context.Response.Close();
                }
                catch (Exception)
                {
                    // Client disconnected or some other error - ignored for this example
                }
            }
        }
    }
}
