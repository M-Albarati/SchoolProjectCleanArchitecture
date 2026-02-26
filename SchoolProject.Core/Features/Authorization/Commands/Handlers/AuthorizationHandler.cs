using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class AuthorizationHandler: ResponseHandler,
                                       IRequestHandler<AddRoleCommand,Response<string>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constractor
        public AuthorizationHandler(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService; 
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.AddRoleAsync(request.RoleName);
            if (role == "Succes") return Success("");
            return BadRequest<string>();
        }
        #endregion
    }
}
