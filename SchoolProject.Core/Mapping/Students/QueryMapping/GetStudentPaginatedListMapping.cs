using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentPaginatedListMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResponse>()
               .ForMember(des => des.StudID, opt => opt.MapFrom(src => src.StudID))
               .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(des => des.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(des => des.DeparmentName, opt => opt.MapFrom(src => src.Department.DName));

        }


    }
}
