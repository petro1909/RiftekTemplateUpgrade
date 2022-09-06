using Microsoft.AspNetCore.Mvc;
using RiftekTemplateUpgrade.Model;
using RiftekTemplateUpgrade.Service;

namespace RiftekTemplateUpgrade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScannerController : ControllerBase
    {
        private readonly ScannerService ScannerService;

        public ScannerController(ScannerService scannerService)
        {
            ScannerService = scannerService;
        }

        [HttpGet("GetScannerSettings")]
        public async Task<ScannerSettings> GetScannerSettings()
        {
            //string jsonScannerSettings = await JsonService.ReadJsonFromApi(ScannerService.ScannerParameterApi);
            return await ScannerService.GetScannerSettingsFromApi();
        }

    //    [HttpGet("SetSettings")]
    //    public void SetScannerSettings()
    //    {
    //        ScannerSettings settings = TemplateService.Templates[1].ScannerSettings;
    //        ScannerService.SetScannerSettings(settings);
    //    }
    }
}
