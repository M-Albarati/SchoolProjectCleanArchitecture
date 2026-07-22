using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Commands.Handlers
{
    internal class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
                                                       , IRequestHandler<EditUserCommand, Response<string>>
                                                       , IRequestHandler<DeleteUserCommand, Response<string>>
                                                       , IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly UserManager<User> _usermanager;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constractor
        public UserCommandHandler(UserManager<User> usermanager,
                                 IMapper mapper,
                                 IApplicationUserService applicationUserService)
        {
            _usermanager = usermanager;
            _mapper = mapper;
            _applicationUserService = applicationUserService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            
            // Create User With Password
            var IdentityUser = _mapper.Map<User>(request);
            var result = await _applicationUserService.AddUserAsync(IdentityUser, request.Password);

            switch (result)
            {
                //Faild
                case "UserName Is Exist": return BadRequest<string>(result);
                case "Email Is Exist": return BadRequest<string>(result);
                case "Failed to Send Email Confirmation": return BadRequest<string>(result);
                case "Failed To Create User": return BadRequest<string>(result);
                
                    //Sucess Created And Send Email Confirmation
                case "Created": return Created("Created");
                default: return BadRequest<string>(result);
            }
            


        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //var OldUser = _usermanager.FindByIdAsync(request.Id.ToString());
            var OldUser = await _usermanager.Users.FirstOrDefaultAsync(x=> x.Id  == request.Id);
            // User Not Exist => NotFound
            if (OldUser == null)
            {
                return NotFound<string>("User ID Not Exist");
            }
            // check UserName is Exist for anather User
            var userByUserName = await _usermanager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName
                                                                               && x.Id != request.Id );
            if (userByUserName != null)
            {
                return BadRequest<string>("UserName Is Exist");
            }

            // Update User
            var UserMapper = _mapper.Map(request,OldUser);
            
            var result = await _usermanager.UpdateAsync(UserMapper);
            //Faild
            if (!result.Succeeded)
            {

                return BadRequest<string>(result.ToString());
            }
            return Updated("");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //var OldUser = _usermanager.FindByIdAsync(request.Id.ToString());
            var user = await _usermanager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            // User Not Exist => NotFound
            if (user == null)
            {
                return NotFound<string>("User ID Not Exist");
            }
            
            var result = await _usermanager.DeleteAsync(user);
            //Faild
            if (!result.Succeeded)
            {

                return BadRequest<string>(result.ToString());
            }
            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //var OldUser = _usermanager.FindByIdAsync(request.Id.ToString());
            var user = await _usermanager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            // User Not Exist => NotFound
            if (user == null)
            {
                return NotFound<string>("User ID Not Exist");
            }
            // check UserName is Exist for anather User
            var userByUserName = await _usermanager.Users.FirstOrDefaultAsync(x => (x.UserName == request.UserName)
                                                                               && (x.Id != request.Id) );
            if (userByUserName != null)
            {
                return BadRequest<string>("UserName Mismatch");
            }

            // Update User Password
            var result = await _usermanager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //Faild
            if (!result.Succeeded)
            {

                return BadRequest<string>(result.ToString());
            }
            return Updated("Password Changed");
        }


        #endregion

    }
}
