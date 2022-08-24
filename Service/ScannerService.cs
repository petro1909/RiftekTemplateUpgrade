using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;

namespace RiftekTemplateUpgrade.Service
{
    public class ScannerService
    {
        public const string ScannerParameterApi = "http://192.168.1.30/api/v1/config/params/values";

        public ScannerSettings GetScannerSettingsFromJson(string json)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            ScannerSettings settings = JsonConvert.DeserializeObject<ScannerSettings>(json, serializerSettings);
            return settings;
        }


        public void SetScannerSettings(ScannerSettings scannerSettings)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ScannerParameterApi);
            client.PutAsJsonAsync(client.BaseAddress, scannerSettings);
        }
    }
}
