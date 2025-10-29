using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepositry;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepositry = departmentRepository; 
        }
        #endregion

        #region Handel Function
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepositry.GetTableNoTracking()
                                                 .Where(x => x.DID.Equals(id))
                                                 .Include(x => x.Students)
                                                 .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                 .Include(x => x.Instructors)
                                                 .Include(x => x.Instructor)
                                                 .FirstOrDefaultAsync();  // when Null --> use Default 
        }

        public async Task<bool> IsIdExist(int id)
        {
            return await _departmentRepositry.GetTableNoTracking().AnyAsync(x=> x.DID.Equals(id));                                   
           
        }
        #endregion

    }
}
