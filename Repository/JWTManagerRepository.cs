using JWT.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWT.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
    {
        { "user1","password1"},
        { "user2","password2"},
        { "user3","password3"},
    };

        public JWTManagerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Tokens Authenticate(Users users)
        {
            if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
            {
                return null;
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
          {
             new Claim(ClaimTypes.Name, users.Name)
          }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(TokenDescriptor);
            return new Tokens { Token = tokenhandler.WriteToken(token) };

        }
    }
}
