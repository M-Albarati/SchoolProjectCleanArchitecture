using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class AuthorizationQueryHandler : ResponseHandler,
    IRequestHandler<GetRoleListQuery, Response<List<GetRoleListResponse>>>,
    IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
    IRequestHandler<ManageUserRolesDataQuery, Response<ManageUserRolesDataResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _useranager;
        #endregion

        #region Constractor
        public AuthorizationQueryHandler(IAuthorizationService authorizationService,
                                         IMapper mapper,
                                         UserManager<User> useranager)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _useranager = useranager;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetRoleListResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roleList = await _authorizationService.GetRoleListAsync();
            var rolesMapper = _mapper.Map<List<GetRoleListResponse>>(roleList);
            return Success(rolesMapper);

        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if(role == null) { return BadRequest<GetRoleByIdResponse>(" Not Exist"); }
            var roleMapper = _mapper.Map<GetRoleByIdResponse>(role);
            return Success(roleMapper);
        }

        public async Task<Response<ManageUserRolesDataResponse>> Handle(ManageUserRolesDataQuery request, CancellationToken cancellationToken)
        {
            // check User is Exist
            var User = await _useranager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (User == null)
            {
                return NotFound<ManageUserRolesDataResponse>("User Not Found");
            };
            // return List Of Roles with True on Exist User Roles
            var result = await _authorizationService.ManageUserRolesData(request.UserId);
            
            return Success(result);
           
        }
    }

        #endregion

  
}
