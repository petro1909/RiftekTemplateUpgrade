using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;

namespace RiftekTemplateUpgrade.Service
{
    public class TemplateService
    {

        public List<Template> Templates;
        public TemplateService()
        {
            Templates = new List<Template>();
        }

        public List<Template> GetTemplatesFromJson(string json)
        {

            List<Template> templates = JsonConvert.DeserializeObject<List<Template>>(json);
            Templates = templates;
            return templates;
        }

        public string WriteTemplatesToJson(List<Template> templates)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(templates, settings);
        } 
    }
}
