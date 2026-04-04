using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<string> EditRoleAsync(int Id , string Name);
        Task<bool> IsExistsAsync(string roleName);
    }
}
