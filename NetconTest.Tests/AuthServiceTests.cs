using FluentAssertions;
using NetconTest.Api.Auth;

namespace NetconTest.Tests
{
    public class AuthServiceTests
    {
        [Fact]
        public void GenerateToken_ShouldReturnTokenForValidCredentials()
        {
            var service = new AuthService();
            var token = service.GenerateToken("admin", "admin");
            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GenerateToken_ShouldReturnNullForInvalidCredentials()
        {
            var service = new AuthService();
            var token = service.GenerateToken("admin", "wrong_password");
            token.Should().BeNull();
        }
    }
}
