using EntidadesCompartidas;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace APIRest.Security
{
    public class TokenService
    {

        private readonly IConfiguration _config;
        private readonly byte[] _key;


        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));
        }



        public string GenerateToken(Usuario unUsuario)
        {
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, unUsuario.UsuarioID.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config.GetValue<string>("Jwt:Issuer"),
                audience: _config.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _config.GetValue<string>("Jwt:Issuer"),
                    ValidateAudience = true,
                    ValidAudience = _config.GetValue<string>("Jwt:Audience"),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
