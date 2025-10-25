using AutoMapper;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile  
    {
        public void GetDepartmentByIdMapping()
        {

            CreateMap<Student, StudentResponse>()

             .ForMember(des => des.Id, opt => opt.MapFrom(src => src.StudID))
             .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));

            
            CreateMap<DepartmetSubject, SubjectResponse>()

             .ForMember(des => des.Id, opt => opt.MapFrom(src => src.SubID))
             .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Subject.SubjectName));

            CreateMap<Instructor, InstructorResponse>()

            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.InsId))
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.ENameAr));

            CreateMap<Department, GetDepartmentByIdResponse>()

              .ForMember(des => des.Id, opt => opt.MapFrom(src => src.DID))
              .ForMember(des => des.Name, opt => opt.MapFrom(src => src.DName))
              .ForMember(des => des.DepartmentManager, opt => opt.MapFrom(src => src.Instructor.ENameAr))
              .ForMember(des => des.StudentList, opt => opt.MapFrom(src => src.Students))
              .ForMember(des => des.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects)) 
              .ForMember(des => des.InstructorList, opt => opt.MapFrom(src => src.Instructors));

        }
    }
}
