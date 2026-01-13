namespace NetconTest.Application.DTOs
{
    public class AssetResponseDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public required double Radius { get; set; }
    }
}
