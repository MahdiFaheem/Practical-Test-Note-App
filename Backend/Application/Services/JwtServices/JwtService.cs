using Application.Interfaces.EncyptionInterfaces;
using Application.Services.JwtServices;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    class JwtService : IAuthenticationService
    {
        private readonly IJwtConfigurationServiceModel _jwtConfigurationServiceModel;

        public JwtService(IJwtConfigurationServiceModel jwtConfigurationServiceModel)
        {
            _jwtConfigurationServiceModel = jwtConfigurationServiceModel;
        }

        public Claim[] Claims { get; set; }

        private Claim[] GenerateClaims(User user)
        {
            return Claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, "AccessToken")
            };
        }

        private JwtSecurityToken GetSecurityToken(Claim[] claims, bool refreshToken = false)
        {
            return new JwtSecurityToken(
                issuer: _jwtConfigurationServiceModel.Issuer,
                audience: _jwtConfigurationServiceModel.Audience,
                expires: DateTime.UtcNow.Add(_jwtConfigurationServiceModel.AccessTokenValidationTime), // Token validation time.
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurationServiceModel.Key)), SecurityAlgorithms.HmacSha256)
            );
        }

        public string CreateToken(User user)
        {
            try
            {
                var claims = GenerateClaims(user);
                var token = GetSecurityToken(claims);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                throw;
            }
        }

        public string CreateRefreshToken(User user)
        {
            try
            {
                var claims = GenerateClaims(user);
                var refreshToken = GetSecurityToken(claims, true);

                return new JwtSecurityTokenHandler().WriteToken(refreshToken);
            }
            catch
            {
                throw;
            }
        }

        public List<Claim> GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurationServiceModel.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtConfigurationServiceModel.Issuer,
                    ValidAudience = _jwtConfigurationServiceModel.Audience,

                    // Set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return jwtToken.Claims.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string VerifyToken(string token)
        {
            try
            {
                var claims = GetClaimsFromToken(token);

                return claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
