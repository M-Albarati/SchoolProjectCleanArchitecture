using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        #endregion

        #region Constructor
        public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            //_UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userManager = userManager;
        }
        #endregion

        #region Handel Function
        public  async Task<JwtAuthResult> GetJWTToken(User user)
        {
            // Generate jwtToken, accessToken
            var (jwtToken, accessToken) = GenerateJWTToken(user);
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
         private (JwtSecurityToken, string) GenerateJWTToken(User user)
        {
            //string issuer = null, string audience = null, IEnumerable<Claim> claims = null
            //, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null
            var Claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims:Claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature) //HmacSha256Signature
                                                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
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
        public List<Claim> GetClaims(User user)
        {
            var Claims = new List<Claim>()
            {
                new Claim (nameof(UserClaimModel.UserId),user.Id.ToString()),
                new Claim (nameof(UserClaimModel.UserName),user.UserName),
                new Claim (nameof(UserClaimModel.Email),user.Email),
                new Claim (nameof(UserClaimModel.PhoneNumber),user.PhoneNumber)
            };
            return Claims;
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            // ReadJWTToken
            var jwtSecurityToken = ReadJWTToken(accessToken);

            // Validation
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature)) ////HmacSha256Signature
            {
                throw new SecurityTokenException("Algorithm Is Wrong",null);
            }
            if (jwtSecurityToken.ValidTo > DateTime.UtcNow)
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
            var (jwtToken,NewAccess) = GenerateJWTToken(user);
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

        public JwtSecurityToken ReadJWTToken(string accessToken)
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
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
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
        #endregion
    }
}
