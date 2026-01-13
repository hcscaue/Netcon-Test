using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetconTest.Application.DTOs;
using NetconTest.Application.Services;

namespace NetconTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/feasibility")]
    [Produces("application/json")]
    public class FeasibilityController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public FeasibilityController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] double? latitude,
            [FromQuery] double? longitude,
            [FromQuery] int? radius
        )
        {
            var requestId = Guid.NewGuid().ToString();
            if (Response != null)
            {
                Response.Headers.Add("X-Request-Id", requestId);
                Response.Headers.Add("Cache-Control", "no-store");
            }
            
            if (!latitude.HasValue)
                return BadRequest(CreateError("empty field", "latitude is mandatory"));

            if (!longitude.HasValue)
                return BadRequest(CreateError("empty field", "longitude is mandatory"));

            if (!radius.HasValue)
                return BadRequest(CreateError("empty field", "radius is mandatory"));

            if (latitude < -90 || latitude > 90)
                return BadRequest(
                    CreateError("invalid value", "latitude must be between -90 and 90")
                );

            if (longitude < -180 || longitude > 180)
                return BadRequest(
                    CreateError("invalid value", "longitude must be between -180 and 180")
                );

            if (radius < 10 || radius > 1000)
                return BadRequest(
                    CreateError("invalid value", "radius must be between 10 and 1000 meters")
                );

            var assets = _assetService.FindAssetsInRadius(
                latitude.Value,
                longitude.Value,
                radius.Value
            );

            return Ok(assets);
        }

        private ErrorResponseDto CreateError(string reason, string message)
        {
            return new ErrorResponseDto
            {
                Code = "400",
                Reason = reason,
                Message = message,
                Status = "bad request",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            };
        }
    }
}
