using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Commands.Validations
{
    public class ChangeUserPasswordValidator: AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly UserManager<User> _useranager;
        #endregion

        #region Constractor
        public ChangeUserPasswordValidator(UserManager<User> useranager)
        {
            _useranager = useranager;
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }
        #endregion

        #region Actions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty")
                .MaximumLength(20).WithMessage("Max Length is 20");

            RuleFor(x => x.CurrentPassword)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty")
                .MinimumLength(8).WithMessage("Mini Length is 8");

            RuleFor(x => x.NewPassword)
               .NotNull().WithMessage("Must Be Not Null")
               .NotEmpty().WithMessage("Must Be Not Empty")
               .MinimumLength(8).WithMessage("Mini Length is 8");

            RuleFor(x => x.ConfirmPassword)
              .NotNull().WithMessage("Must Be Not Null")
              .NotEmpty().WithMessage("Must Be Not Empty")
              .Equal(x => x.NewPassword).WithMessage("Not Equal NewPassword");
        }

        public void ApplyCustomValidationRoles()
        {
            RuleFor(x => x.Id).MustAsync(async (key, CancellationToken) => await _useranager.Users.AnyAsync(x => x.Id.Equals(key)))
                .WithMessage(" This User Not Exist");
        }
        #endregion
    }
}
