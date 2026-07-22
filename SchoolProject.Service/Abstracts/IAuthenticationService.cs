using Microsoft.AspNetCore.Server.HttpSys;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtSecurityToken> ReadJWTToken(string accessToken);
        public Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken);
        public Task<string> ValidateToken(string accessToken);
        public Task<string> ConfirmEmail(int? userId, string? code);
    }
}
