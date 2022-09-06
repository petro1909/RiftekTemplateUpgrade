using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiftekTemplateUpgrade.Service;
using RiftekTemplateUpgrade.FanucSocket;
using System.Net;
using System.Text.Json.Serialization;

namespace RiftekTemplateUpgrade.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly ScannerService ScannerService;
        private readonly SocketTcpServer SocketTcpServer;
        public ConnectionController(ScannerService scannerService, SocketTcpServer socketTcpServer) 
        {
            ScannerService = scannerService;
            SocketTcpServer = socketTcpServer;
        }

        [HttpGet("GetDefaultConnectionParameters")]
        public  async Task<ConnectionSettings> GetDefaultConnectionParameters()
        {
            ConnectionSettings settings = new ConnectionSettings(
                SocketTcpServer.DesktopAddress.ToString(),
                SocketTcpServer.DesktopPort,
                ScannerService.ScannerIpAdress.ToString());
            return settings;
        }

        [HttpPost("SetConnectionParameters")]
        public void SetConnectionParameters(ConnectionSettings settings)
        {

            SocketTcpServer.DesktopPort = settings.DesktopPort;
            ScannerService.ScannerIpAdress = IPAddress.Parse(settings.ScannerAddress);
            SocketTcpServer.Restart();
        }



        public class ConnectionSettings
        {
            public string DesktopAddress { get; set; }
            public int DesktopPort { get; set; }
            public string ScannerAddress { get; set; }

            public ConnectionSettings()
            {

            }

            public ConnectionSettings(string desktopAddress, int desktopPort, string scannerAddress)
            {
                DesktopAddress = desktopAddress;
                DesktopPort = desktopPort;
                ScannerAddress = scannerAddress;
            }

        }
    }
}
