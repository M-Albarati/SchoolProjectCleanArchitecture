using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        public  Task<Department> GetDepartmentByIdAsync(int id);
        public  Task<bool> IsIdExist(int id);
    }
}
