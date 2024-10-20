using Api.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Servise
{
    public class JwtTokenGenerator
    {
        private readonly string sekretKey;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            this.sekretKey = configuration["AuthSettings:SecretKey"];
        }
        public string GenerateJwtToken(AppUser appUser, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(sekretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {new Claim("id",appUser.Id),
               new Claim(ClaimTypes.Email,appUser.Email),
               new Claim(ClaimTypes.Role, String.Join(",", roles)),
               }),

                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
               SecurityAlgorithms.EcdsaSha512Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
    //    private readonly string secretKey;

    //    public JwtTokenGenerator(IConfiguration configuration)
    //    {
    //        this.secretKey = configuration["AuthSettings:SecretKey"];
    //    }

    //    public string GenerateJwtToken(AppUser appUser, IList<string> roles)
    //    {
    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(secretKey);

    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(new Claim[]{
    //                //new Claim("FistName", appUser.FirstName),
    //                new Claim("id", appUser.Id),
    //                new Claim(ClaimTypes.Email, appUser.Email),
    //                new Claim(ClaimTypes.Role, String.Join(",", roles))
    //            }),

    //            Expires = DateTime.UtcNow.AddDays(1),

    //            SigningCredentials = new SigningCredentials(
    //                new SymmetricSecurityKey(key),
    //                SecurityAlgorithms.HmacSha512Signature
    //            )
    //        };

    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //        return tokenHandler.WriteToken(token);
    //    }
    //}
}
