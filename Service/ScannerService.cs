using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;
using System.Net;
using RiftekTemplateUpgrade.Worker;
using System.Reflection;

namespace RiftekTemplateUpgrade.Service
{
    public class ScannerService
    {
        public static IPAddress ScannerIpAdress = IPAddress.Parse("192.168.1.30");
        public static string ScannerParameterApi = $"http://{ScannerIpAdress}/api/v1/config/params/values";


        public ScannerService()
        {

        }

        public async Task<ScannerSettings> GetScannerSettingsFromApi()
        {
            string jsonScannerSettings = await JsonWorker.ReadJsonFromApi(ScannerParameterApi);
            return GetScannerSettingsFromJson(jsonScannerSettings);
        }

        private ScannerSettings GetScannerSettingsFromJson(string json)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            ScannerSettings settings = JsonConvert.DeserializeObject<ScannerSettings>(json, serializerSettings);
            return settings;
        }


        public async Task<HttpResponseMessage> SetScannerSettings(ScannerSettings scannerSettings)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ScannerParameterApi);


            List<KeyValuePair<string, string>> queryParameters1 = new List<KeyValuePair<string, string>>();
            PropertyInfo[] properties = scannerSettings.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string pr = null;
                if (property.PropertyType.BaseType == typeof(System.Enum))
                {
                    pr = ((int)property.GetValue(scannerSettings)).ToString();
                }
                else
                {
                    pr = property.GetValue(scannerSettings).ToString();
                }
                KeyValuePair<string, string> pair = new KeyValuePair<string, string>(property.Name, pr);
                queryParameters1.Add(pair);
            }

            var uri = QueryHelpers.AddQueryString(ScannerParameterApi, queryParameters1);
            var request = new HttpRequestMessage(HttpMethod.Put, uri);

            var response = await  client.SendAsync(request);
            return response;
            
        }


        public async void SetScannerSettings(int templateNumber)
        {
            TemplateService templateService = new TemplateService();
            ScannerSettings settings = templateService.GetTemplatesFromFile().Single(t => t.Number == templateNumber).ScannerSettings;
            await SetScannerSettings(settings);
            
            
        }

    }
}
