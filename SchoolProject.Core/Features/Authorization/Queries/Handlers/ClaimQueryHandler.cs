using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimQueryHandler : ResponseHandler,
                                   IRequestHandler<ManageUserClaimsDataQuery, Response<ManageUserClaimsDataResponse>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        //private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;
        #endregion

        #region constructors
        public ClaimQueryHandler(IAuthorizationService authorizationService,
                                         //IMapper mapper,
                                         UserManager<User> usermanager)
        {
            _authorizationService = authorizationService;
            //_mapper = mapper;
            _usermanager = usermanager;
        }
        #endregion

        #region Handle Functions

        #endregion
        public async Task<Response<ManageUserClaimsDataResponse>> Handle(ManageUserClaimsDataQuery request, CancellationToken cancellationToken)
        {
            // check User is Exist
            var User = await _usermanager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (User == null)
            {
                return NotFound<ManageUserClaimsDataResponse>("User Not Found");
            };
            // return List Of Roles with True on Exist User Roles
            var result = await _authorizationService.ManageUserClaimsData(request.UserId);

            return Success(result);
        }
    }
}
