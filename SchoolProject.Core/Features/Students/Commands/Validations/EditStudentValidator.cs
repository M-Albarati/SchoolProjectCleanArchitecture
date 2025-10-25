using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {

        #region Fields
        private readonly IStudentService _studentService;

        #endregion

        #region Constractor
        public EditStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;

            ApplyValidationRoles();
            ApplyCustomValidationRoles();

        }
        #endregion

        #region Actions
        public void ApplyValidationRoles()
        {

            RuleFor(x => x.StudID)
                .NotNull().WithMessage("Name Must Be Not Null")
                .NotEmpty().WithMessage("Name Must Be Not Empty");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name Must Be Not Null")
                .NotEmpty().WithMessage("Name Must Be Not Empty")
                .MaximumLength(10).WithMessage("Name Max Lendth is 10");
           
            RuleFor(x => x.Address)
                .NotNull().WithMessage("{PropertyName} Must Be Not Null")
                .NotEmpty().WithMessage("{PropertyName} Must Be Not Empty")
                .MaximumLength(50).WithMessage("{PropertyName} Max Length is 50");
        }

        public void ApplyCustomValidationRoles()
        {
            RuleFor(x => x.StudID).MustAsync(async (key, CancellationToken) => await _studentService.IsIdExist(key))
                .WithMessage(" This Student Not Exist");
        }
        #endregion



    }
}
