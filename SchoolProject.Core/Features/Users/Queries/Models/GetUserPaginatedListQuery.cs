using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Responses;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Queries.Models
{
    public class GetUserPaginatedListQuery : IRequest<PaginatedResult<GetUserPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
