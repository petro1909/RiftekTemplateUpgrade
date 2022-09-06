using Microsoft.AspNetCore.Mvc;
using RiftekTemplateUpgrade.Model;
using RiftekTemplateUpgrade.Service;
using HtmlAgilityPack;
using RiftekTemplateUpgrade.FanucSocket;
using System.Net;
using System.Net.NetworkInformation;
using RiftekTemplateUpgrade.Worker;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace RiftekTemplateUpgrade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        //private const string TestFilePath = "C:\\Users\\BENFIN\\source\\repos\\Riftek\\RiftekTemplateUpgrade\\resources\\txt.json";
        string TemplatesFilePath;
        ScannerService ScannerService;
        TemplateService TemplateService;
        SocketTcpServer SocketTcpServer;


        public TemplateController(ScannerService scannerService, TemplateService templateService, SocketTcpServer socketTcpServer)
        {
            ScannerService = scannerService;
            TemplateService = templateService;
            SocketTcpServer = socketTcpServer;
        }

        [HttpGet("Templates")]
        public JsonResult GetTemplates()
        {
            List<Template> templates = TemplateService.GetTemplatesFromFile();
            return new JsonResult(templates);
            //return templates;
        }


        [HttpPost("SaveTempalteSettings")]
        public void SaveTemplateSettings([FromBody]Template template) 
        {
            TemplateService.UpdateTemplate(template);
        }

        [HttpGet("AddTemplate")]
        public async Task AddTemplate()
        {
            ScannerSettings settings = await ScannerService.GetScannerSettingsFromApi();
            TemplateService.AddTemplate(settings);
        }

        [HttpGet("DeleteTemplate")]
        public void DeleteTemplate(int number)
        {
           TemplateService.DeleteTemplate(number);
        }

        //For Tests

        //[HttpGet("FullTemplates")]
        //public IActionResult FullTemplates()
        //{
        //    ScannerSettings settings = GetScannerSettings().Result;

        //    TemplateService.Templates = new List<Template>(19);
        //    for(int i = 0; i < 18; i++)
        //    {
        //        Template template = new Template(i, settings);
        //        TemplateService.Templates.Add(template);
        //    }

        //    string templatesJson = TemplateService.WriteTemplatesToJson(TemplateService.Templates);
        //    JsonService.WriteJsonToFile(templatesJson , TemplatesFilePath);
        //    return RedirectToAction("GetTemplates");
        //}
    }
}