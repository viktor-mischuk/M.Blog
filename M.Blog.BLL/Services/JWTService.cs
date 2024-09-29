using M.Blog.BLL.Interfaces;
using M.Blog.DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace M.Blog.BLL.Services
{
    internal class JWTService(IOptions<AuthSettings> options)
    {
        public string GenerateToken(User user) 
        {
            var claims = new List<Claim>
            {
                new Claim("userName", user.Name),
                new Claim("userEmail", user.Email),
                new Claim("id", user.Id.ToString()),
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            
            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                        SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
