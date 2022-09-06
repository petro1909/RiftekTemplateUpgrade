using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;
using RiftekTemplateUpgrade.Worker;

namespace RiftekTemplateUpgrade.Service
{
    public class TemplateService
    {
        public string TemplatesFilePath = $"{Directory.GetCurrentDirectory()}\\resources\\templates.json";
        public List<Template> Templates;
        public TemplateService()
        {
            Templates = new List<Template>();
        }

        public List<Template> GetTemplatesFromFile()
        {
            string templatesJson = JsonWorker.ReadJsonFromFile(TemplatesFilePath);
            return GetTemplatesFromJson(templatesJson);
        }
        public List<Template> GetTemplatesFromFile(string filePath)
        {
            string templatesJson = JsonWorker.ReadJsonFromFile(filePath);
            return GetTemplatesFromJson(templatesJson);
        }

        private List<Template> GetTemplatesFromJson(string json)
        {
            List<Template> templates = JsonConvert.DeserializeObject<List<Template>>(json);
            Templates = templates;
            return templates;
        }
        public void WriteTemplatesToFile(List<Template> templates)
        {
            string templatesJson = WriteTemplatesToJson(templates);
            JsonWorker.WriteJsonToFile(templatesJson, TemplatesFilePath);
        }
        public void WriteTemplatesToFile(List<Template> templates, string filePath)
        {
            string templatesJson = WriteTemplatesToJson(templates);
            JsonWorker.WriteJsonToFile(templatesJson, filePath);
        }
        public string WriteTemplatesToJson(List<Template> templates)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(templates, settings);
        } 


        public void DeleteTemplate(int number)
        {
            if (number == -1) return;
            Template template = Templates.Single(t => t.Number == number);
            int index = Templates.IndexOf(template);
            Templates.Remove(template);
            for (int i = index;  i < Templates.Count; i++)
            {
                Templates[i].Number--;
            }
            WriteTemplatesToFile(Templates, TemplatesFilePath);
        }

        public void AddTemplate(ScannerSettings settings)
        {
            int greaterTemplateNumber = Templates[^1].Number;
            Template template = new Template(greaterTemplateNumber + 1, settings);
            Templates.Add(template);
            WriteTemplatesToFile(Templates, TemplatesFilePath);
        }

        public void UpdateTemplate(Template template)
        {
            Template actualTemplate = Templates.Single(t => t.Number == template.Number);
            actualTemplate.ScannerSettings = template.ScannerSettings;
            WriteTemplatesToFile(Templates, TemplatesFilePath);
        }

    }
}
