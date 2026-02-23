using FluentValidation;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Users.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Commands.Validations
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields

        #endregion

        #region Constractor
        public SignInValidator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }
        #endregion

        #region Actions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty")
                .MaximumLength(20).WithMessage("Max Length is 20");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty")
                .MinimumLength(8).WithMessage("Mini Length is 8");
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
