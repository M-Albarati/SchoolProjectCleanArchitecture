using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SchoolProject.Data.DTOs.ManageUserRolesDataResponse;

namespace SchoolProject.Service.Implementations
{
    internal class AuthorizationService: IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constractor
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<string> DeleteRoleAsync(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null) return "Not Found";

            // check users has this role
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users != null && users.Count() > 0) return "Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Succes";
            // Fiald
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<string> EditRoleAsync(int Id, string Name)
        {
            // check role Exist
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null) return "Not Found";

            role.Name = Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Succes";
            // Fiald
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<bool> IsExistByNameAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> IsExistByIdAsync(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null) return false;
            else return true;
        }

        public async Task<List<Role>> GetRoleListAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int Id)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }

        public async Task<ManageUserRolesDataResponse> ManageUserRolesData(int UserId)
        {
            var user =await _userManager.FindByIdAsync(UserId.ToString());
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var response = new ManageUserRolesDataResponse();

            var roleList = new List<Roles>();
            foreach (var role in roles)
            {
                var userRole = new Roles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                userRole.HasRole = userRoles.Contains(role.Name.ToString());
                roleList.Add(userRole);
            }
            response.UserId = user.Id;
            response.RoleList = roleList;

            return response;
        }
        #endregion
    }
}

