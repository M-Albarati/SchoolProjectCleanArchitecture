using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Data;
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
        private readonly AppDbContext _DbContext;
        #endregion

        #region Constractor
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    AppDbContext DbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _DbContext = DbContext;
        }
        #endregion

        #region Handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var role = new Role();
            role.Name = roleName;

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return "Success";
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
            if (result.Succeeded) return "Success";
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
            if (result.Succeeded) return "Success";
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

            var roleList = new List<UserRole>();
            foreach (var role in roles)
            {
                var userRole = new UserRole();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                userRole.HasRole = userRoles.Contains(role.Name.ToString());
                roleList.Add(userRole);
            }
            response.UserId = user.Id;
            response.RoleList = roleList;

            return response;
        }

        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
           var transact = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                // Check User
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "User Not Found";

                //Remove Old User Roles
                var userRoles = await _userManager.GetRolesAsync(user);
                var Remresult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!Remresult.Succeeded) return "Faild Remove Old Roles";

                var Newroles = request.RoleList.Where(y => y.HasRole == true).Select(x => x.Name);
                var Addresult = await _userManager.AddToRolesAsync(user, Newroles);
                if (!Addresult.Succeeded) return "Faild Add New Roles";
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transact.RollbackAsync();
                return "Faild Update User Roles";
            }
           
        }
        #endregion
    }
}

