namespace NetconTest.Domain.Entities
{
    public class Asset
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required List<Geometry> Geometry { get; init; } = new();

        public double? Latitude => Geometry.FirstOrDefault()?.Y;
        public double? Longitude => Geometry.FirstOrDefault()?.X;
    }
}
