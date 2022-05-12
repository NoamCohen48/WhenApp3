﻿using System.Security.Claims;
using whenAppModel.Models;
using WhenUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using whenAppModel.Services;

namespace WebApplication1.Services
{
    public class JWTService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly WhenAppContext _context;

        public JWTService(WhenAppContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        public async Task<string> AssignToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim("UserId", username)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWTParams:Issuer"],
                _configuration["JWTParams:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: mac
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}