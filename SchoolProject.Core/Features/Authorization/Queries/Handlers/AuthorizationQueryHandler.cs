using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
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
    IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public AuthorizationQueryHandler(IAuthorizationService authorizationService,
                                         IMapper mapper)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
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

        #endregion

    }
}
