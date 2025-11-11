using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Commands.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
       
        #endregion

        #region Constractor
        public AddUserValidator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }
        #endregion

        #region Actions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.FullName)
                .NotNull().WithMessage("Name Must Be Not Null")
                .NotEmpty().WithMessage("Name Must Be Not Empty")
                .MaximumLength(50).WithMessage("Name Max Lendth is 50");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("{PropertyName} Must Be Not Null")
                .NotEmpty().WithMessage("{PropertyName} Must Be Not Empty")
                .MaximumLength(20).WithMessage("{PropertyName} Max Length is 20");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("{PropertyName} Must Be Not Null")
                .NotEmpty().WithMessage("{PropertyName} Must Be Not Empty");

            RuleFor(x => x.Password)
               .NotNull().WithMessage("{PropertyName} Must Be Not Null")
               .NotEmpty().WithMessage("{PropertyName} Must Be Not Empty")
               .MinimumLength(8).WithMessage("{PropertyName} Mini Length is 8");

            RuleFor(x => x.ConfirmPassword)
              .NotNull().WithMessage("{PropertyName} Must Be Not Null")
              .NotEmpty().WithMessage("{PropertyName} Must Be Not Empty")
              .Equal(x=> x.Password).WithMessage("{PropertyName} Not Equal Password");
        }

        public void ApplyCustomValidationRoles()
        {
            //RuleFor(x => x.Password).MustAsync(async (key, CancellationToken) => !await _usermanager.e(key))
            //    .WithMessage(" This Name Is Exist");

            //RuleFor(x => x.UserName).MustAsync(async (key, CancellationToken) => !await _departmentService.IsIdExist(key))
            //    .WithMessage(" Department Id Is Not Exist");
        }
        #endregion
    }
}
