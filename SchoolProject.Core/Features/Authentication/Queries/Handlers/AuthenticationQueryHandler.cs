using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<ValidateTokenQuery, Response<string>>
    {

        #region Fields
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constractor
        public AuthenticationQueryHandler(IAuthenticationService authenticationService)
        {
        
            _authenticationService = authenticationService;

        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            return Success(result);
        }
        #endregion
    }
}