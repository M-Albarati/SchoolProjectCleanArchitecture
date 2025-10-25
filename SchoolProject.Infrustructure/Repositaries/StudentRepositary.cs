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
    public class StudentRepositary : GenericRepositoryAsync<Student>, IStudentRepository
    {

        #region Feilds
        private readonly DbSet<Student> _student;
        #endregion

        #region Constractor
        public StudentRepositary(AppDbContext dbcontext): base(dbcontext)
        {
            _student = dbcontext.Set<Student>();
        }

        #endregion

        #region Handle Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _student.Include(x => x.Department).ToListAsync();
        }

        //public override async Task<List<Student>> GetListAsync()
        //{
        //    return await _student.Include(x => x.Department).ToListAsync();
        //}
        #endregion

    }
}
