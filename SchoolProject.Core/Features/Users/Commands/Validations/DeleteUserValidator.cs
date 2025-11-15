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
    public class DeleteUserValidator:AbstractValidator<DeleteUserCommand>
    {
        #region Fields
        private readonly UserManager<User> _useranager;
        #endregion

        #region Constractor
        public DeleteUserValidator (UserManager<User> useranager)
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
                .NotNull().WithMessage("{PropertyName} Must Be Not Null")
                .NotEmpty().WithMessage("{PropertyName} Must Be Not Empty");
        }

        public void ApplyCustomValidationRoles()
        {
            RuleFor(x => x.Id).MustAsync(async (key, CancellationToken) => await _useranager.Users.AnyAsync(x => x.Id.Equals(key)))
                .WithMessage(" This User Not Exist");
        }
        #endregion

    }
}
