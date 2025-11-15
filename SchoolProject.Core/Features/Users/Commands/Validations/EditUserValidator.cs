using FluentValidation;
using SchoolProject.Core.Features.Users.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Commands.Validations
{
    public class EditUserValidator: AbstractValidator<EditUserCommand>
    {
        #region Fields

        #endregion

        #region Constractor
        public EditUserValidator()
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
