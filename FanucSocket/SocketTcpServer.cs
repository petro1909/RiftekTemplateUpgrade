using System.Net.Sockets;
using System.Net;
using System.Text;
using RiftekTemplateUpgrade.Service;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using RiftekTemplateUpgrade.Model;
using Microsoft.AspNetCore.DataProtection;

namespace RiftekTemplateUpgrade.FanucSocket
{
    public class SocketTcpServer
    {
        public static IPAddress DesktopAddress = GetDesctopIpAddress();
        public static int DesktopPort = 56001;

        private ScannerService _scannerSerive;
        private TemplateService _templateService;
        //public static CancellationTokenSource cts;
        //public static Task task;
        //public static CancellationToken token;
        public static Thread thread;
        public static object stop = false;


        public SocketTcpServer(ScannerService scannerService, TemplateService templateService)
        {
            _scannerSerive = scannerService;
            _templateService = templateService;
            thread = new Thread(RecieveMessages);
            thread.Start();

        }


        private static IPAddress GetDesctopIpAddress()
        {
            Regex regex = new Regex("^192\\.168\\.[0-9]{1,3}\\.[0-9]{1,3}$");
            try
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                NetworkInterface networkInterface = networkInterfaces.Single(n => n.Name == "Ethernet");
                IPInterfaceProperties properties = networkInterface.GetIPProperties();
                var iPAddress = properties.UnicastAddresses.Single(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork && regex.IsMatch(ip.Address.ToString())).Address;
                return iPAddress;
            } catch(Exception e)
            {
                return IPAddress.Parse("192.168.1.62");
            }
            //return IPAddress.Parse("192.168.1.62");
        }


        public void Restart()
        {
            SendMessage();
            stop = true;
            thread = new Thread(RecieveMessages);
            thread.Start();
        }

        public void SendMessage()
        {
            try 
            {
                IPEndPoint ipPoint = new IPEndPoint(DesktopAddress, DesktopPort);
                // создаем сокет
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                byte[] data = new byte[] { 255 };
                socket.Send(data);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void RecieveMessages()
        {
                IPEndPoint ipPoint = new IPEndPoint(DesktopAddress, DesktopPort);
                // создаем сокет
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    // связываем сокет с локальной точкой, по которой будем принимать данные
                    listenSocket.Bind(ipPoint);

                    // начинаем прослушивание
                    listenSocket.Listen(10);
                    // Console.WriteLine("Сервер запущен. Ожидание подключений...");
                    while (!(bool)stop)
                    {
                        //if(token.IsCancellationRequested)
                        //{
                        //    return;
                        //}
                        Socket handler = listenSocket.Accept();
                        // получаем сообщение
                        byte[] data = new byte[16]; // буфер для получаемых данных

                        do
                        {
                            handler.Receive(data);
                            int templateNumber = data[0];
                            Template template = _templateService.GetTemplatesFromFile().Single(t => t.Number == templateNumber);
                            await _scannerSerive.SetScannerSettings(template.ScannerSettings);
                        }
                        while (handler.Available > 0);
                        // отправляем ответ
                        string message = "ваше сообщение доставлено";
                        //data = Encoding.Unicode.GetBytes(message);
                        data[1] = 2;
                        handler.Send(data);
                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }           
        }

    }
}
