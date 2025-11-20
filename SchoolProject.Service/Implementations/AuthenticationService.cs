using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructor
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Handel Function
        public  Task<string> GetJWTToken(User user)
        {
            //string issuer = null, string audience = null, IEnumerable<Claim> claims = null
            //, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null
            var Claims = new List<Claim>()
            {
                new Claim (nameof(UserClaimModel.UserName),user.UserName),
                new Claim (nameof(UserClaimModel.Email),user.Email),
                new Claim (nameof(UserClaimModel.PhoneNumber),user.PhoneNumber)
            };
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
                                                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(accessToken);  // not async task
        }
        #endregion
    }
}
