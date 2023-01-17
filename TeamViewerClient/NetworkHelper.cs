using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TeamViewerClient
{
    internal class NetworkHelper
    {
        public static void Start(string ipString,int port)
        {
            var client = new TcpClient();
            var ip = IPAddress.Parse(ipString);
            var ep = new IPEndPoint(ip, port);

            try
            {
                client.Connect(ep);
                if (client.Connected)
                {
                    MessageBox.Show("Connected");
                    var writer = Task.Run(() =>
                    {
                        while (true)
                        {
                            var text = "I Connect";
                            var stream = client.GetStream();
                            var bw = new BinaryWriter(stream);
                            bw.Write(text);
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

