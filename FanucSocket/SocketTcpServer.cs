using System.Net.Sockets;
using System.Net;
using System.Text;
using RiftekTemplateUpgrade.Service;

namespace RiftekTemplateUpgrade.FanucSocket
{
    public class SocketTcpServer
    {
        const int port = 56001;

        public void RecieveMessages(TemplateService templateService)
        {
            Thread thread = new Thread(() =>
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.1.10"), port);

                // создаем сокет
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    // связываем сокет с локальной точкой, по которой будем принимать данные
                    listenSocket.Bind(ipPoint);

                    // начинаем прослушивание
                    listenSocket.Listen(10);

                    Console.WriteLine("Сервер запущен. Ожидание подключений...");
                    while (true)
                    {
                        Socket handler = listenSocket.Accept();
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0; // количество полученных байтов
                        byte[] data = new byte[16]; // буфер для получаемых данных

                        do
                        {
                            bytes = handler.Receive(data);
                            int template = data[0];
                            ScannerService scannerService = new ScannerService();
                            scannerService.SetScannerSettings(templateService.Templates[template].ScannerSettings);
                            //builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                            for (byte b = 0; b < bytes; b++)
                            {
                                Console.Write($"{data[b]} ");
                            }
                            Console.WriteLine();
                        }
                        while (handler.Available > 0);

                        //Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                        //// отправляем ответ
                        //string message = "ваше сообщение доставлено";
                        //data = Encoding.Unicode.GetBytes(message);
                        //handler.Send(data);
                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            thread.Start();

            
        }

    }
}
