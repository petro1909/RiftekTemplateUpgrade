using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RiftekTemplateUpgrade.Model
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExposureControl
    {
        [EnumMember(Value = "Auto")]
        Auto,
        [EnumMember(Value = "Fixed")]
        Fixed,
        [EnumMember(Value = "Adjust")] 
        Adjust,
        [EnumMember(Value = "2 Exposures")]
        _2_Exposures,
        [EnumMember(Value = "3 Expoures")]
        _3_Exposures,
        [EnumMember(Value = "Differences")]
        Differences
    }
}
