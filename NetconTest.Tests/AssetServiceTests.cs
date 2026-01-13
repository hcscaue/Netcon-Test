using FluentAssertions;
using Moq;
using NetconTest.Application.Services;
using NetconTest.Domain.Entities;
using NetconTest.Domain.Repositories;

namespace NetconTest.Tests
{
    public class AssetServiceTests
    {
        [Fact]
        public void FindAssetsInRadius_ShouldReturnNearbyAssets()
        {
            var mockRepo = new Mock<IAssetRepository>();
            mockRepo
                .Setup(r => r.GetAll())
                .Returns(
                    new List<Asset>
                    {
                        new Asset
                        {
                            Id = 1,
                            Name = "CTO-1",
                            Geometry = new List<Geometry>
                            {
                                new Geometry { X = -46.630000, Y = -23.550000 },
                            },
                        },
                        new Asset
                        {
                            Id = 2,
                            Name = "CTO-2",
                            Geometry = new List<Geometry>
                            {
                                new Geometry { X = -46.640000, Y = -23.560000 },
                            },
                        },
                    }
                );

            var service = new AssetService(mockRepo.Object);

            var result = service.FindAssetsInRadius(-23.556456, -46.635653, 1000);

            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
        }

        [Fact]
        public void FindAssetsInRadius_ShouldReturnEmptyWhenNoAssets()
        {
            var mockRepo = new Mock<IAssetRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Asset>());

            var service = new AssetService(mockRepo.Object);

            var result = service.FindAssetsInRadius(-23.556456, -46.635653, 100);

            result.Should().BeEmpty();
        }

        [Fact]
        public void FindAssetsInRadius_ShouldFilterByRadius()
        {
            var mockRepo = new Mock<IAssetRepository>();
            mockRepo
                .Setup(r => r.GetAll())
                .Returns(
                    new List<Asset>
                    {
                        new Asset
                        {
                            Id = 1,
                            Name = "Near",
                            Geometry = new List<Geometry>
                            {
                                new Geometry { X = -46.635653, Y = -23.556456 },
                            },
                        },
                        new Asset
                        {
                            Id = 2,
                            Name = "Far",
                            Geometry = new List<Geometry>
                            {
                                new Geometry { X = -46.700000, Y = -23.600000 },
                            },
                        },
                    }
                );

            var service = new AssetService(mockRepo.Object);

            var result = service.FindAssetsInRadius(-23.556456, -46.635653, 50);

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Near");
        }

        [Fact]
        public void FindAssetsInRadius_ShouldSkipAssetsWithNullCoordinates()
        {
            var mockRepo = new Mock<IAssetRepository>();
            mockRepo
                .Setup(r => r.GetAll())
                .Returns(
                    new List<Asset>
                    {
                        new Asset
                        {
                            Id = 1,
                            Name = "Valid",
                            Geometry = new List<Geometry>
                            {
                                new Geometry { X = -46.635653, Y = -23.556456 },
                            },
                        },
                        new Asset
                        {
                            Id = 2,
                            Name = "Invalid",
                            Geometry = new List<Geometry>
                            {
                                new Geometry { X = null, Y = null },
                            },
                        },
                    }
                );

            var service = new AssetService(mockRepo.Object);

            var result = service.FindAssetsInRadius(-23.556456, -46.635653, 1000);

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Valid");
        }
    }
}
