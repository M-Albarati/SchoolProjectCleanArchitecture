using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppControllerBase //{ControllerBase}  handle ObjectResult ReturnCode = StatusCode
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

        [HttpPost(Router.AuthorizationRoute.Create)]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        #endregion
    }
}
