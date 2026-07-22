using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        //private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public AuthenticationService(JwtSettings jwtSettings,
                                     IUserRefreshTokenRepository userRefreshTokenRepository,
                                     UserManager<User> userManager,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _jwtSettings = jwtSettings;
            //_UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Handel Function
        public  async Task<JwtAuthResult> GetJWTToken(User user)
        {
            // Generate jwtToken, accessToken
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            // Generate RefreshToken
            var refreshToken = GetRefreshToken(user.UserName);

            // Generate object from UserRefreshToken
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                Token = accessToken,
                RefreshToken = refreshToken.TokenString,
                UserId = user.Id
            };
            // save To UserRefreshToken Table
            await _userRefreshTokenRepository.AddAsync(userRefreshToken);

            // Generate JwtAuthResult Result
            var jwtAuthResult = new JwtAuthResult
            {
                AccessToken = accessToken,
                refreshToken = refreshToken
            };
       
            return (jwtAuthResult);
        }

        // Generate jwtToken, accessToken
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            //string issuer = null, string audience = null, IEnumerable<Claim> claims = null
            //, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null
            var Claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256) //HmacSha256Signature
                                                );
            
            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(jwtToken);
            var jwtToken2 = handler.ReadJwtToken(accessToken);
            return (jwtToken, accessToken);
        }
        
        // Generate RefreshToken
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                UserName = username,
                ExpirAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                TokenString = GenerateRefreshToken()
            };
            //_UserRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }

        // GenerateRandomNumber
        private string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            var RandomNumberGenerate = RandomNumberGenerator.Create();
            RandomNumberGenerate.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }

        // GetClaims
        public async Task<List<Claim>> GetClaims(User user)
        {
            var UserRoles = await _userManager.GetRolesAsync(user);
            var UserClaims = await _userManager.GetClaimsAsync(user);
            var Claims = new List<Claim>()
            {
                new Claim (nameof(UserClaimModel.UserId),user.Id.ToString()),
                new Claim (nameof(UserClaimModel.UserName),user.UserName),
                new Claim (nameof(UserClaimModel.Email),user.Email),
                new Claim (nameof(UserClaimModel.PhoneNumber),user.PhoneNumber)
            };

            //Add User Roles to  Token claims
            foreach (var role in UserRoles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            //Add User claims to  Token claims
            Claims.AddRange(UserClaims);

            return Claims;
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            // ReadJWTToken
            var jwtSecurityToken = await ReadJWTToken(accessToken);
            
            // Validation
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256)) ////HmacSha256Signature
            {
                throw new SecurityTokenException("Algorithm Is Wrong",null);
            }
            
            //if (jwtSecurityToken.ValidTo > DateTime.UtcNow)
            if (jwtSecurityToken.ValidTo > DateTime.Now)
            {
                throw new SecurityTokenException("Token Is  NotExpired", null);
            }

            //Get User Id
            var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserId)).Value;
            var userRefreshToken = await _userRefreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                throw new SecurityTokenException("Refresh Token Is Not Found", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh Token Is Expired", null);
            }
            
            //Get User
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SecurityTokenException("User Is Not Found", null);
            }

            // Generate Access Token
            var (jwtToken,NewAccess) = await GenerateJWTToken(user);
            var OldRefresh = new RefreshToken
            {
                ExpirAt = userRefreshToken.ExpiryDate,
                UserName = user.UserName,
                TokenString = refreshToken
            };
            var jwtAuthResult = new JwtAuthResult
            {
                AccessToken = NewAccess,
                refreshToken = OldRefresh
            };
            return jwtAuthResult;

        }

        public async Task<JwtSecurityToken> ReadJWTToken(string accessToken)
        {
            // AccessToken Is null
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));
          
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public  Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuer = _jwtSettings.Issuer ,
                ValidIssuers = new []{ _jwtSettings.Issuer},
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidAudiences = new[] { _jwtSettings.Audience },
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            var validator =  handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try
            {
                if (validatedToken == null)
                    throw new SecurityTokenException("Invalid Token");
                return Task.FromResult( "Valid");
            }
            catch (Exception ex )
            {
                return Task.FromResult(ex.Message);
                throw;
            }
        }

        public async Task<string> ConfirmEmail(int? userId, string? code)
        {
            if (userId == null || code == null)
                return ("Invalid Code Or UserId");
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);

            if (!confirmEmail.Succeeded)
                return ("Unconfirmed Email");
            else return ("Confirmed Email");
    }
        #endregion
    }
}
