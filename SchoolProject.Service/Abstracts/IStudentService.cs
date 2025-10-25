using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        //public Task<List<Student>> GetListAsync();
        public IQueryable<Student> GetStudentsPaginatedQueryable();
        public IQueryable<Student> FilterStudentsPaginatedQueryable(string? search, StudentOrderingEnum? orderby);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsIdExist(int id);


    }
}
