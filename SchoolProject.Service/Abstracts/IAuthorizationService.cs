using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
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
        Task<string> EditRoleAsync(int Id, string Name);
        Task<string> DeleteRoleAsync(int Id);
        Task<List<Role>> GetRoleListAsync();
        Task<Role> GetRoleByIdAsync(int Id);
        Task<bool> IsExistByNameAsync(string roleName);
        Task<bool> IsExistByIdAsync(int Id);
        Task<ManageUserRolesDataResponse> ManageUserRolesData(int UserId);
    }
}
