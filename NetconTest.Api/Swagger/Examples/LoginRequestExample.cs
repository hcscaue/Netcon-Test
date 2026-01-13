using NetconTest.Api.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace NetconTest.Api.Swagger.Examples
{
    public class LoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest { Name = "admin", Password = "admin" };
        }
    }
}
