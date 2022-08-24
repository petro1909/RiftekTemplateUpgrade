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

        public TemplateController(JsonService jsonService, ScannerService scannerService, TemplateService templateService)
        {
            TemplatesFilePath = $"{Directory.GetCurrentDirectory()}\\resources\\templates.json";
            JsonService = jsonService;
            ScannerService = scannerService;
            TemplateService = templateService;
        }

        [HttpGet("Templates")]
        public IEnumerable<Template> GetTemplates()
        {
            string jsonTemplates = JsonService.ReadJsonFromFile(TemplatesFilePath);
            List<Template> templates = TemplateService.GetTemplatesFromJson(jsonTemplates);
            return templates;
        }

        [HttpGet("GetScannerSettings")]
        public async Task<ScannerSettings> GetScannerSettings()
        {
            string jsonScannerSettings = await JsonService.ReadJsonFromApi(ScannerService.ScannerParameterApi);
            return ScannerService.GetScannerSettingsFromJson(jsonScannerSettings);
        }

        [HttpGet("SetSettings")]
        public void SetScannerSettings()
        {
            ScannerSettings settings = TemplateService.Templates[1].ScannerSettings;
            ScannerService.SetScannerSettings(settings);
        }


        [HttpPost("SaveTempalteSettings")]
        public void SaveTemplateSettings([FromBody]Template template) 
        {
            TemplateService.Templates[template.Number] = template;
            string templatesJson = TemplateService.WriteTemplatesToJson(TemplateService.Templates);
            JsonService.WriteJsonToFile(templatesJson, TemplatesFilePath);
        }

        [HttpGet("FullTemplates")]
        public IActionResult FullTemplates()
        {
            ScannerSettings settings = GetScannerSettings().Result;

            TemplateService.Templates = new List<Template>(19);
            for(int i = 0; i < 18; i++)
            {
                Template template = new Template(i, settings);
                TemplateService.Templates.Add(template);
            }

            string templatesJson = TemplateService.WriteTemplatesToJson(TemplateService.Templates);
            JsonService.WriteJsonToFile(templatesJson , TemplatesFilePath);
            return RedirectToAction("GetTemplates");
        }
    }
}