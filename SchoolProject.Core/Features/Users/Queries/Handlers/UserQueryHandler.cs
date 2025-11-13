using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Features.Users.Queries.Models;
using SchoolProject.Core.Features.Users.Responses;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Queries.Handlers
{
    internal class UserQueryHandler: ResponseHandler, IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListResponse>>,
                                                      IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly UserManager<User> _usermanager;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public UserQueryHandler(UserManager<User> usermanager, IMapper mapper)
        {
            _usermanager = usermanager;
            _mapper = mapper;
        }


        #endregion
         #region Handle Functions
        public Task<PaginatedResult<GetUserPaginatedListResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var UserListQueryable = _usermanager.Users.AsQueryable();
            var PaginatedList = _mapper.ProjectTo<GetUserPaginatedListResponse>(UserListQueryable)
                                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _usermanager.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>();
            }
            var userMapper = _mapper.Map<GetUserByIdResponse>(user);
            return Success(userMapper);
        }

        #endregion
    }
}
