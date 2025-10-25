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
    public class SubjectRepository : GenericRepositoryAsync<Subject>,ISubjectRepository
    {
        #region Fields
        private DbSet<Subject> _subject;
        #endregion

        #region Constructor
        public SubjectRepository(AppDbContext dbContext) : base(dbContext)
        {
            _subject = dbContext.Set<Subject>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}
