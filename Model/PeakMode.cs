using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace RiftekTemplateUpgrade.Model
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum PeakMode
    {
        [EnumMember(Value = "Max intesity")]
        Max_intensity,
        [EnumMember(Value = "First")]
        First,
        [EnumMember(Value = "Last")]
        Last,
        [EnumMember(Value = "Prefer 2")] 
        Prefer_2,
        [EnumMember(Value = "Prefer 3")]
        Prefer_3,
        [EnumMember(Value = "Prefer 4")]
        Prefer_4
    }
}
