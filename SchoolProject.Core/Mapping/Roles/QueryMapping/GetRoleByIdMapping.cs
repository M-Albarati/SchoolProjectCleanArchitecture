using AutoMapper;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
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
        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResponse>();
               //.ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
               //.ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
