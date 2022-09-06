
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace RiftekTemplateUpgrade.Model
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum EdrMode
    {
        [EnumMember(Value = "Edr Disabled")]
        Edr_disabled,
        [EnumMember(Value = "Column Edr")]
        Column_edr,
        [EnumMember(Value = "Piecewise Linear Edr")]
        Piecewise_linear_EDR
    }
}
