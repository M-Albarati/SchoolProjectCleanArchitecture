using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IInstructorRepository: IGenericRepositoryAsync<Instructor>
    {
    }
}
