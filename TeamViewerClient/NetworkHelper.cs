using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace TeamViewerClient
{
    internal class NetworkHelper
    {
        public static string ExitCommand = "exit";
        public static void WriteDataToServer(string text)
        {
            Thread thread = new Thread(() =>
            {
                MessageBox.Show("Exited");
                try
                {

                    var stream = client.GetStream();
                    var bw = new BinaryWriter(stream);
                    bw.Write(text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            thread.Start();
        }
        private static TcpClient client = new TcpClient();
        public static void Start(string ipString, int port)
        {
            client = new TcpClient();
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
                            try
                            {
                                Task.Delay(10);

                                ImageHelper helper = new ImageHelper();
                                var path = helper.TakeScreenShot();
                                var bytes = helper.GetBytesOfImage(path);

                                var stream = client.GetStream();
                                stream.Write(bytes, 0, bytes.Length);

                            }
                            catch (Exception)
                            {
                            }
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

