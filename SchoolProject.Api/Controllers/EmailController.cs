using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class EmailController : AppControllerBase
    {
        #region Fields
        //private readonly IMediator _mediator;
        #endregion

        #region Constractor
        //public AuthorizationController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}
        #endregion

        // تم استبدال
        // ( _mediator => Mediator)
        // الذي في الكلاس
        // (AppControllerBase)

        #region Handel Function

        //[Authorize(Roles = "Admin")]  // Admin And User
        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin,User")]  // Admin or User
        [HttpPost(Router.EmailRoute.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        #endregion
    }
}
