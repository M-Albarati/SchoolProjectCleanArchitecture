using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Commands.Handlers
{
    internal class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
                                                       , IRequestHandler<EditUserCommand, Response<string>>
    {
        #region Fields
        private readonly UserManager<User> _usermanager;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public UserCommandHandler(UserManager<User> usermanager, IMapper mapper)
        {
            _usermanager = usermanager;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // check UserName is Exist
            var userByUserName = await _usermanager.FindByNameAsync(request.UserName);
            if (userByUserName != null)
            {
                return BadRequest<string>("UserName Is Exist");
            }
            // check Email is Exist
            var userByEmai = await _usermanager.FindByEmailAsync(request.Email);
            if (userByEmai != null)
            {
                return BadRequest<string>("Email Is Exist");
            }
            // Create User With Password
            var usermapper = _mapper.Map<User>(request);
            var result =await _usermanager.CreateAsync(usermapper, request.Password);
            //Faild
            if (!result.Succeeded)
            {

                return BadRequest<string>(result.ToString());
            }
            //Sucess
                return Created("Created"); 
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
        #endregion

    }
}
