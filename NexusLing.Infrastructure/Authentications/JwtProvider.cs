using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexusLing.Infrastructure.Authentications
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string Generate(UserDTO user)
        {
            var claims = new Claim[]
            {
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (JwtRegisteredClaimNames.UniqueName, user.Login),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, null, DateTime.UtcNow.AddHours(1), signingCredentials);
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
