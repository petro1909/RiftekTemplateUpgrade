using Microsoft.AspNetCore.Mvc;
using RiftekTemplateUpgrade.Model;
using RiftekTemplateUpgrade.Service;
using HtmlAgilityPack;

namespace RiftekTemplateUpgrade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private const string TestFilePath = "C:\\Users\\BENFIN\\source\\repos\\Riftek\\RiftekTemplateUpgrade\\resources\\txt.json";
        string TemplatesFilePath;
        JsonService JsonService;
        ScannerService ScannerService;
        TemplateService TemplateService;
        List<Template> Templates;

        public TemplateController(JsonService jsonService, ScannerService scannerService, TemplateService templateService)
        {
            TemplatesFilePath = $"{Directory.GetCurrentDirectory()}\\resources\\templates.json";
            JsonService = jsonService;
            ScannerService = scannerService;
            TemplateService = templateService;
            Templates = new List<Template>();
        }

        [HttpGet("Templates")]
        public IEnumerable<Template> GetTemplates()
        {
            string jsonTemplates = JsonService.ReadJsonFromFile(TemplatesFilePath);
            Templates = TemplateService.GetTemplatesFromJson(jsonTemplates);
            return Templates;
        }

        [HttpGet("GetScannerSettings")]
        public async Task<ScannerSettings> GetScannerSettings()
        {
            string jsonScannerSettings = await JsonService.ReadJsonFromApi(ScannerService.ScannerParameterApi);
            return ScannerService.GetScannerSettingsFromJson(jsonScannerSettings);
        }

        [HttpPost("SaveTempalteSettings")]
        public void SaveTemplateSettings(Template template) 
        {
            Templates.Insert(template.Number, template);
            string templatesJson = TemplateService.WriteTemplatesToJson(Templates);
            JsonService.WriteJsonToFile(templatesJson, TemplatesFilePath);
        }

        [HttpGet("FullTemplates")]
        public IActionResult FullTemplates()
        {
            ScannerSettings settings = GetScannerSettings().Result;

            Templates = new List<Template>(19);
            for(int i = 0; i < 18; i++)
            {
                Template template = new Template(i, settings);
                Templates.Add(template);
            }

            string templatesJson = TemplateService.WriteTemplatesToJson(Templates);
            JsonService.WriteJsonToFile(templatesJson , TemplatesFilePath);
            return RedirectToAction("GetTemplates");
        }
    }
}