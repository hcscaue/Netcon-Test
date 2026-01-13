using System.Text.Json;
using NetconTest.Domain.Entities;
using NetconTest.Domain.Repositories;
using NetconTest.Infra.Models;

namespace NetconTest.Infra.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly string _jsonPath;

        public AssetRepository(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public List<Asset> GetAll()
        {
            var json = File.ReadAllText(_jsonPath);

            var jsonModels = JsonSerializer.Deserialize<List<AssetJsonModel>>(json) ?? [];

            return jsonModels
                .Select(j => new Asset
                {
                    Id = j.Id,
                    Name = j.Name,
                    Geometry = j
                        .Geometry.Select(g => new Geometry
                        {
                            X = g.X,
                            Y = g.Y,
                            Z = g.Z,
                        })
                        .ToList(),
                })
                .ToList();
        }
    }
}
