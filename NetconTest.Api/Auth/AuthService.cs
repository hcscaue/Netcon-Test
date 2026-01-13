using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NetconTest.Api.Auth
{
    public interface IAuthService
    {
        string? GenerateToken(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private const string SecretKey = "sua-chave-secreta-super-segura-minimo-32-caracteres";
        private const string ValidUsername = "admin";
        private const string ValidPassword = "admin";

        public string? GenerateToken(string username, string password)
        {
            if (username != ValidUsername || password != ValidPassword)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
