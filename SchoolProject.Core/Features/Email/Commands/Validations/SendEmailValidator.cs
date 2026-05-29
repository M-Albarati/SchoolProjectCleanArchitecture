using FluentValidation;
using SchoolProject.Core.Features.Email.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Email.Commands.Validations
{
    public class SendEmailValidator: AbstractValidator<SendEmailCommand>
    {
        #region Fields
        #endregion

        #region Ctor
        public SendEmailValidator()
        {
            ApplyValidationRoles();
        }
        #endregion

        #region Handel Actions
        public void ApplyValidationRoles()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email Must Be Not Null")
                .NotEmpty().WithMessage("Email Must Be Not Empty")
                .EmailAddress().WithMessage("Email Must Be Avalid Email");
            RuleFor(x => x.Message)
                .NotNull().WithMessage("Message Must Be Not Null")
                .NotEmpty().WithMessage("Message Must Be Not Empty");
        }
        #endregion

    }
}
