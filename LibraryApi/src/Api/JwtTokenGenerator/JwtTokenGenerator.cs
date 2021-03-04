using Application.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.JwtTokenGenerator
{
    public static class JwtTokenGenerator
    {
        public static string GenerateJsonWebToken(UserWithRoleDto user, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim (ClaimTypes.Role, user.RoleName)
            };

            var token =
                new JwtSecurityToken(
                        config["Jwt:Issuer"],
                        config["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddDays(7),
                        signingCredentials: credentials
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);

    
        }
    }
}
