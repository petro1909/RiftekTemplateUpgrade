using RiftekTemplateUpgrade.Model;
using Newtonsoft.Json;

namespace RiftekTemplateUpgrade.Service
{
    public class TemplateService
    {
        public List<Template> GetTemplatesFromJson(string json)
        {

            List<Template> templates = JsonConvert.DeserializeObject<List<Template>>(json);
            return templates;
        }

        public string WriteTemplatesToJson(List<Template> templates)
        {
            return JsonConvert.SerializeObject(templates);
        } 
    }
}
