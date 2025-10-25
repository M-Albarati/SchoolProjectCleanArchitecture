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
    public class InstructorRepository : GenericRepositoryAsync<Instructor>,IInstructorRepository
    {
        #region Fields
        private DbSet<Instructor> _instructor;
        #endregion

        #region Constructor
        public InstructorRepository(AppDbContext dbContext) : base(dbContext)
        {
            _instructor = dbContext.Set<Instructor>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}
