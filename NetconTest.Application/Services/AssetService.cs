using NetconTest.Application.DTOs;
using NetconTest.Domain.Entities;
using NetconTest.Domain.Repositories;

namespace NetconTest.Application.Services
{
    public interface IAssetService
    {
        List<AssetResponseDto> FindAssetsInRadius(double latitude, double longitude, int radius);
    }

    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;

        public AssetService(IAssetRepository repository)
        {
            _repository = repository;
        }

        public List<AssetResponseDto> FindAssetsInRadius(
            double latitude,
            double longitude,
            int radius
        )
        {
            var assets = _repository.GetAll();
            var result = new List<AssetResponseDto>();

            foreach (var asset in assets)
            {
                if (!asset.Latitude.HasValue || !asset.Longitude.HasValue)
                    continue;

                var distance = CalculateHaversineDistance(
                    latitude,
                    longitude,
                    asset.Latitude.Value,
                    asset.Longitude.Value
                );

                if (distance <= radius)
                {
                    result.Add(
                        new AssetResponseDto
                        {
                            Id = asset.Id,
                            Name = asset.Name,
                            Latitude = asset.Latitude.Value,
                            Longitude = asset.Longitude.Value,
                            Radius = Math.Round(distance, 2),
                        }
                    );
                }
            }

            return result;
        }

        private double CalculateHaversineDistance(
            double lat1,
            double lon1,
            double lat2,
            double lon2
        )
        {
            const double R = 6371000;

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                + Math.Cos(DegreesToRadians(lat1))
                    * Math.Cos(DegreesToRadians(lat2))
                    * Math.Sin(dLon / 2)
                    * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
