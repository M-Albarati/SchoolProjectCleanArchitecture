using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    
    public class AuthenticationController : AppControllerBase //{ControllerBase}  handle ObjectResult ReturnCode = StatusCode
    {
        #region Fields
        //private readonly IMediator _mediator;
        #endregion

        #region Constractor
        //public DepartmentController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}
        #endregion

        // تم استبدال
        // ( _mediator => Mediator)
        // الذي في الكلاس
        // (AppControllerBase)

        #region Handel Function

        [HttpPost(Router.AuthRoute.SignIn)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        
        [HttpPost(Router.AuthRoute.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }

        [HttpGet(Router.AuthRoute.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] ValidateTokenQuery Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        #endregion
    }
}