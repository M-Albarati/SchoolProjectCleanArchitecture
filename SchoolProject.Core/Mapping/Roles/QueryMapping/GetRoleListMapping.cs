using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleListMapping()
        {
            CreateMap<Role, GetRoleListResponse>();
               //.ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
               //.ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
