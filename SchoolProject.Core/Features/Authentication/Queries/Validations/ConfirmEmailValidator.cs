using FluentValidation;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Queries.Validations
{
    public class ConfirmEmailValidator: AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailValidator()
        {
            ApplyValidationRoles();
        }

        public void ApplyValidationRoles()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty");

            RuleFor(x => x.Code)
                .NotNull().WithMessage("Must Be Not Null")
                .NotEmpty().WithMessage("Must Be Not Empty");
        }

    }
}