using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validations
{
    public class DeleteRoleValidators: AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constractor
        public DeleteRoleValidators(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }
        #endregion

        #region Actions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Name Must Be Not Null")
                .NotEmpty().WithMessage("Name Must Be Not Empty");
        }

        public void ApplyCustomValidationRoles()
        {
            //RuleFor(x => x.Id).MustAsync(async (key, CancellationToken) => await _authorizationService.IsExistByIdAsync(key))
            //    .WithMessage("Not Exist");
        }
        #endregion
    }
}

