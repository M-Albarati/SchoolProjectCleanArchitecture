using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Email.Commands.Handlers
{
    public class EmailCommandHandler : ResponseHandler,
                                     IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IEmailService _emailService;
        #endregion

        #region ctor
        public EmailCommandHandler(IEmailService emailService)
        {
                _emailService = emailService;
        }
        #endregion
        #region Handle Actions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Message,"");
            if (response == "Success")
            {
                return Success<string>(response);
            }
            else 
                return BadRequest<string>(response);
        }
        #endregion

    }
}
