using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;

namespace RiftekTemplateUpgrade.Service
{
    public class JsonService
    {
        //private const string filePath = "C:\\Users\\BENFIN\\source\\repos\\Riftek\\RiftekTemplateUpgrade\\resources\\txt.json";

        //public ScannerSettings GetScannerSettingsFromJsonFile()
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.NullValueHandling = NullValueHandling.Ignore;
        //    ScannerSettings settings = null;

        //    using (StreamReader sr = new StreamReader(filePath))
        //    {
        //        using (JsonTextReader jtr = new JsonTextReader(sr))
        //        {
        //            settings = serializer.Deserialize<ScannerSettings>(jtr);
        //        }
        //    }
        //    return settings;
        //}

        public async Task<string> ReadJsonFromApi(string api)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(api);
            var response = await client.GetAsync(client.BaseAddress);
            return await response.Content.ReadAsStringAsync();
        }

        public string ReadJsonFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                 return reader.ReadToEnd();
            }
        }

        public void WriteJsonToFile(string json, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(json);
            }
        }
    }
}
