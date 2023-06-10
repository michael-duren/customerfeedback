using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomerFeedback.Models;
using Microsoft.IdentityModel.Tokens;

namespace CustomerFeedback.Services
{
    public class TokenService
    {
        public string CreateToken(AppUser user)
        {
            // create the claims for the JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // create a new symmetric key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "4e0cb400-3528-4b28-b455-67388c2fc2583309767f-589f-4dba-ac91-d6d837a3957a"
                )
            );

            // signing credentials, the key and what alg to use
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(1),
            };

            // create the token handler, and then token
            var tokenHanlder = new JwtSecurityTokenHandler();
            var token = tokenHanlder.CreateToken(tokenDescriptor);

            // return the serialized token
            return tokenHanlder.WriteToken(token);
        }
    }
}
