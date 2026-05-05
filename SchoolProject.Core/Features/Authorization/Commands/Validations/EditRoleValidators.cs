using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validations
{
    public class EditRoleValidators:AbstractValidator<EditRoleCommand>
    {
        #region Fields
       
        #endregion

        #region Constractor
        public EditRoleValidators()
        {
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
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name Must Be Not Null")
                .NotEmpty().WithMessage("Name Must Be Not Empty");
        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
