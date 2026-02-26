using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    internal class AuthorizationService: IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        #endregion

        #region Constractor
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion

        #region Handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var role = new Role();
            role.Name = roleName;
           
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return "Succes";
            return "Fiald";
        }

        public async Task<bool> IsExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
