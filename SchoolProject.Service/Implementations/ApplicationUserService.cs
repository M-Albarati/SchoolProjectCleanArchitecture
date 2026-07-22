using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<User> _usermanager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _appDbContext;
        #endregion

        #region ctor
        public ApplicationUserService(UserManager<User> usermanager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailService emailService,
                                      AppDbContext appDbContext)
        {
            _usermanager = usermanager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _appDbContext = appDbContext;
        }
        #endregion

        #region Handle Functions
        public async Task<string> AddUserAsync(User user, string password)
        {
            var transact = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                // check UserName is Exist
                var userByUserName = await _usermanager.FindByNameAsync(user.UserName);
                if (userByUserName != null)
                {
                    return "UserName Is Exist";
                }
                // check Email is Exist
                var userByEmai = await _usermanager.FindByEmailAsync(user.Email);
                if (userByEmai != null)
                {
                    return "Email Is Exist";
                }

                // Create User With Password
                var result = await _usermanager.CreateAsync(user, password);

                //Failed
                if (!result.Succeeded)
                {
                    return result.Errors.FirstOrDefault().Description;
                }

                // Add new user to User Role
                await _usermanager.AddToRoleAsync(user, "User");

                // send Email Confirmed
                var code = await _usermanager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme
                              + "://" + requestAccessor.Host
                              + $"/Api/V1/Auth/ConfirmEmail?UserId={user.Id}&code={code}";
                var message = $"To Confirm Email Click Link: <a href ='{returnUrl}'></a>";
                // Message body
                var sendEmailResult = await _emailService.SendEmailAsync(user.Email, message, "Email Confirmation");
                // Check Send Email
                if (sendEmailResult != "Success")
                {
                    // Send Email Failed
                    return "Failed to Send Email Confirmation";
                }
                else
                {
                    //Sucess Created And Send Email Confirmation
                    await transact.CommitAsync();
                    return "Created";
                }
                    
            }
            catch (Exception)
            {

                await transact.RollbackAsync();
                return "Failed To Create User";
            }
            
        }
        #endregion

    }
}
