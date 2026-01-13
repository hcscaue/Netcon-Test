using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetconTest.Infra.Models
{
    public class AssetJsonModel
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("geometry")]
        public required List<GeometryJsonModel> Geometry { get; set; }
    }
}
