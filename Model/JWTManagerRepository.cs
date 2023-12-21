using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using COOKING_RECIPE_PORTAL.Data;
using COOKING_RECIPE_PORTAL.Model;

namespace COOKING_RECIPE_PORTAL.Model
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        private LoginContext _db;
        public JWTManagerRepository(IConfiguration iconfiguration, LoginContext context)
        {
            this.iconfiguration = iconfiguration;
            _db = context;
        }

        public MyJwtToken Authenticate(string email, string password)
        {
            var u = _db.Login.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (u == null)
                return null;
                
            //Session["UserID"] = u.Id.ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);

            //var jwtConfig = iconfiguration.GetSection("JWT");
            //var tokenKey = Encoding.UTF8.GetBytes(jwtConfig["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor();
            Claim c1 = new Claim("Email", u.Email);
            Claim c2 = new Claim("Password", u.Password);
            ClaimsIdentity cIdentity = new ClaimsIdentity(new Claim[] { c1, c2 });

            tokenDescriptor.Subject = cIdentity;
            tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(10);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new MyJwtToken { Token = tokenHandler.WriteToken(token) };
        }
    }
}
