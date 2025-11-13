using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Features.Users.Responses;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserPaginatedListMapping()
        {
            CreateMap<User,GetUserPaginatedListResponse>()
            .ForMember(des => des.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(des => des.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(des => des.Country, opt => opt.MapFrom(src => src.Country));

        }
    }
}
