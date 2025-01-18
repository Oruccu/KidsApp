using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using KidsAppBackend.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace KidsAppBackend.Business.Utilities
{
    public class JwtTokenGenerator
    {
        private readonly string _secretKey;
        private readonly int _tokenExpirationMinutes;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSettings:SecretKey"]
                ?? throw new ArgumentNullException("JwtSettings:SecretKey is missing in configuration");

            if (!int.TryParse(configuration["JwtSettings:TokenExpirationMinutes"], out _tokenExpirationMinutes))
            {
                throw new ArgumentException("JwtSettings:TokenExpirationMinutes must be a valid integer.");
            }
        }

        public string GenerateToken(ChildUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
