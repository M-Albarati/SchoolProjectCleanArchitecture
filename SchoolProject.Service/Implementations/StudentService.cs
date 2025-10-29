using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepositary;
        #endregion

        #region Constractor
        public StudentService(IStudentRepository studentRepositary)
        {
            _studentRepositary = studentRepositary;
        }

        #endregion

        #region Handel Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepositary.GetStudentsListAsync();
        }
        //public async Task<List<Student>> GetListAsync()
        //{

        //    return await _studentRepositary.GetListAsync();
        //}

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepositary.GetTableNoTracking()
                                     .Include(x => x.Department)
                                     .Where(x => x.StudID.Equals(id))
                                     .FirstOrDefaultAsync(); // when Null --> use Default 
            // .GetByIdAsync(id);
        }

        public async Task<string> AddAsync(Student student)
        {
            //// check Exist
            //var stu = _studentRepositary.GetTableNoTracking().Where(x =>  x.Name.Equals(student.Name)).FirstOrDefault();
            //if (stu != null) { return "Exist"; }

           // Add Student
            try
            {
                await _studentRepositary.AddAsync(student);
                return "Success";
            }
            catch (Exception e)
            {
                var s = e.Message;
                return "Exception";
            }
           
            
        }

        public async Task<bool> IsNameExist(string name)
        {
            // check Exist
            var stu = _studentRepositary.GetTableNoTracking().Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (stu == null) { return false; }
            return true;

        }

        public async Task<string> EditAsync(Student student)
        {
            try
            {
                await _studentRepositary.UpdateAsync(student);
                return "Success";
            }
            catch (Exception e)
            {
                var s = e.Message;
                return "Exception";
            }
        }

        public async Task<bool> IsIdExist(int id)
        {
            // check Exist
            var stu = _studentRepositary.GetTableNoTracking().Where(x => x.StudID.Equals(id)).FirstOrDefault();
            if (stu == null) { return false; }
            return true;

        }

        public async Task<string> DeleteAsync(Student student)
        {
            var Trans = _studentRepositary.BeginTransaction();
            try
            {
                await _studentRepositary.DeleteAsync(student);
                await Trans.CommitAsync();
                return "Success";
            }
            catch (Exception e)
            {
                var s = e.Message;
                await Trans.RollbackAsync();
                return "Exception";
            }
        }

        public IQueryable<Student> GetStudentsPaginatedQueryable()
        {
            return _studentRepositary.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentsPaginatedQueryable(string? search, StudentOrderingEnum? orderby)
        {
            var queryable = _studentRepositary.GetTableNoTracking().Include(x =>x.Department).AsQueryable();
            if (search != null)
            {
                queryable = queryable.Where(x => x.Name.Contains(search) || x.Address.Contains(search));
            }
            if (orderby != null)
            {
                switch (orderby)
                {
                    case StudentOrderingEnum.StudID:
                        queryable = queryable.OrderBy(x => x.StudID);
                        break;
                    case StudentOrderingEnum.Name:
                        queryable = queryable.OrderBy(x => x.Name);
                        break;
                    case StudentOrderingEnum.Address:
                        queryable = queryable.OrderBy(x => x.Address);
                        break;
                    case StudentOrderingEnum.DeparmentName:
                        queryable = queryable.OrderBy(x => x.Department.DName);
                        break;
                    default:
                        break;
                }
            }
            return queryable;
        }
        #endregion
    }
}
