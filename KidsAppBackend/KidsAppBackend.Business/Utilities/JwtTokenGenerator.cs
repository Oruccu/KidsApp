using System;
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
            _secretKey = configuration["JwtSettings:SecretKey"];
            _tokenExpirationMinutes = int.Parse(configuration["JwtSettings:TokenExpirationMinutes"]);
        }

        public string GenerateToken(ChildUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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


// user@example.com
// test1234