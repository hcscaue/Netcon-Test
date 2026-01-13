using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetconTest.Infra.Models
{
    public class GeometryJsonModel
    {
        [JsonPropertyName("x")]
        public double? X { get; set; }

        [JsonPropertyName("y")]
        public double? Y { get; set; }

        [JsonPropertyName("z")]
        public double? Z { get; set; }
    }
}
