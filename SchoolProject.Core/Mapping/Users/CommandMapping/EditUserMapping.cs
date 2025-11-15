using SchoolProject.Core.Features.Users.Commands.Models;
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
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, User>();
            //.ForMember(des => des.FullName, opt => opt.MapFrom(src => src.FullName))
            //.ForMember(des => des.UserName, opt => opt.MapFrom(src => src.UserName))
            //.ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(des => des.Address, opt => opt.MapFrom(src => src.Address))
            //.ForMember(des => des.Country, opt => opt.MapFrom(src => src.Country))
            //.ForMember(des => des.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            // تم الالغاء لان اسماء الحقول نسها 
        }
    }

}
