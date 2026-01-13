using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NetconTest.Api.Controllers;
using NetconTest.Application.Services;

namespace NetconTest.Tests
{
    public class FeasibilityControllerTests
    {
        [Fact]
        public void Get_ShouldReturnBadRequest_WhenLatitudeIsNull()
        {
            var mockService = new Mock<IAssetService>();
            var controller = new FeasibilityController(mockService.Object);

            var result = controller.Get(null, -43.18, 100);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Get_ShouldReturnBadRequest_WhenRadiusIsOutOfRange()
        {
            var mockService = new Mock<IAssetService>();
            var controller = new FeasibilityController(mockService.Object);

            var result = controller.Get(-22.91, -43.18, 5000);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
