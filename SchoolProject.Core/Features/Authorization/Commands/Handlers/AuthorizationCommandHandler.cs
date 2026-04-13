using AutoMapper;
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
    public class AuthorizationCommandHandler : ResponseHandler,
                                       IRequestHandler<AddRoleCommand, Response<string>>,
                                       IRequestHandler<EditRoleCommand, Response<string>>,
                                       IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public AuthorizationCommandHandler(IAuthorizationService authorizationService,
                                           IMapper mapper)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Succes") return Added("");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request.Id, request.Name);
            if (result == "Not Found") return NotFound<string>();
            else if (result == "Succes") return Updated("");
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "Not Found") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>("Role is Used");
            else if (result == "Succes") return Deleted<string>();
            else return BadRequest<string>(result);
        }
        #endregion
    }
}
