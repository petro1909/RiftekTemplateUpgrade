using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;

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

            IEnumerable<KeyValuePair<string,string>> queryParameters = scannerSettings.GetType().GetProperties().Select(p => new KeyValuePair<string, string>(p.Name, p.GetValue(scannerSettings).ToString()));

            var uri = QueryHelpers.AddQueryString(ScannerParameterApi, queryParameters);
            var request = new HttpRequestMessage(HttpMethod.Put, uri);

            var response = client.SendAsync(request);
           
        }
    }
}
