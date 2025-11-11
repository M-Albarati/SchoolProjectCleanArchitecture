using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : AppControllerBase //{ControllerBase}  handle ObjectResult ReturnCode = StatusCode
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

        [HttpPost(Router.UserRoute.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        #endregion
    }
}
