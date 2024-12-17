﻿using DataLayer.Constants.DBContext;
using DataLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Token
{
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public JwtToken(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public string GenerateJwtToken(string userId, string userName)
        {
            var user = _dataContext.User.FirstOrDefault(u=>u.Id == int.Parse(userId) );

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId),
                new Claim(JwtRegisteredClaimNames.Name,userName),
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Role,user.role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: creds
                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
