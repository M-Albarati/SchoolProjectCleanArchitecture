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
    public class AddRoleValidators:AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constractor
        public AddRoleValidators(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }
        #endregion

        #region Handle Actions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.RoleName)
                .NotNull().WithMessage("Name Must Be Not Null")
                .NotEmpty().WithMessage("Name Must Be Not Empty");
        }

        public void ApplyCustomValidationRoles()
        {
            RuleFor(x => x.RoleName).MustAsync(async (key, CancellationToken) => !await _authorizationService.IsExistByNameAsync(key))
                .WithMessage(" This Name Is Exist");
        }
        #endregion
    }
}

