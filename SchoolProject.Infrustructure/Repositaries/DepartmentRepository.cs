using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Bases;
using SchoolProject.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositaries
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Fields
        private DbSet<Department> _department;
        #endregion

        #region Constructor
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _department = dbContext.Set<Department>();
        }
        #endregion

        #region Handel Functions
        #endregion

    }
}
