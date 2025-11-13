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
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            //.ForMember(des => des.UserName, opt => opt.MapFrom(src => src.UserName))
            //.ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(des => des.Address, opt => opt.MapFrom(src => src.Address))
            //.ForMember(des => des.Country, opt => opt.MapFrom(src => src.Country));
            //نفس الحقول لذلك ما يحتاج مابيج
        }
    }
}
