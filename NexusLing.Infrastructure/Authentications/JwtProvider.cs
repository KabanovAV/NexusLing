using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using System.Security.Claims;
using System.Text;

namespace NexusLing.Infrastructure.Authentications
{
    /// <summary>
    /// Провайдер Jwt
    /// </summary>
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _сonfiguration;
        private const string SectionName = "Jwt";

        public JwtProvider(IConfiguration сonfiguration)
        {
            _сonfiguration = сonfiguration;
        }

        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Возвращает токен</returns>
        public string Generate(UserDTO user)
        {
            var secretKey = _сonfiguration[$"{SectionName}:Key"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new (JwtRegisteredClaimNames.UniqueName, user.Login),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_сonfiguration.GetValue<int>($"{SectionName}:ExpirationInMinutes")),
                SigningCredentials = signingCredentials,
                Issuer = _сonfiguration[$"{SectionName}:Issuer"],
                Audience = _сonfiguration[$"{SectionName}:Audience"]
            };

            string tokenValue = new JsonWebTokenHandler().CreateToken(tokenDescriptor);
            return tokenValue;
        }
    }
}
