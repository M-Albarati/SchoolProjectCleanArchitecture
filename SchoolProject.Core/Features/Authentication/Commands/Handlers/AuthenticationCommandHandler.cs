using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler:ResponseHandler,
                                              IRequestHandler<SignInCommand,Response<JwtAuthResult>>,
                                              IRequestHandler<RefreshTokenCommand,Response<JwtAuthResult>>
    {

        #region Fields
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signinmanager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constractor
        public AuthenticationCommandHandler(UserManager<User> usermanager, SignInManager<User> signinmanager, IAuthenticationService authenticationService)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _authenticationService = authenticationService;
        }


        #endregion

        #region Handle Functions
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // Ckeck UserName Is Exist
            var user = await _usermanager.FindByNameAsync(request.UserName);
            // User Not Exist =>  User Not Found
            if (user == null)
            {
                return NotFound<JwtAuthResult>("User ID Not Exist");
            }

            // Try Login And Ckeck Password Is Correct
            var SignInResult = _signinmanager.CheckPasswordSignInAsync(user,request.Password,false);
            //Failed Return Password Not Correct
            if (!SignInResult.Result.Succeeded)
            {
                //return BadRequest<string>(SignInResult.Status.ToString());
                return BadRequest<JwtAuthResult>("Password Is Wrong");
            }
            //Generate JWT Token
            var Result = await _authenticationService.GetJWTToken(user);
            //Return Token
            return Success(Result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return Success(result);
        }
        #endregion
    }
}
