using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    //[Authorize]
        //[Authorize (Roles = "Admin")]
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

            //[Authorize(Roles = "Admin")]  // Admin And User
            //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin,User")]  // Admin or User
        [HttpPost(Router.AuthorizationRoute.Create)]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }

        [HttpPut(Router.AuthorizationRoute.Update)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }

        [HttpDelete(Router.AuthorizationRoute.Delete)]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "تعديل صلاحيات المستخدمين", OperationId = "UpdateUserRoles")]
        [HttpPut(Router.AuthorizationRoute.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRoute.List)]
        public async Task<IActionResult> GetRoleList()
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(new GetRoleListQuery());
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRoute.GetById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            //var response = await _mediator.Send(Command);
            //var response = await Mediator.Send(new GetRoleByIdQuery(id)); // calss with ctor
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = id}); // calss with out ctor
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "ادارة صلاحيات المستخدمين",OperationId = "ManageUserRolesData")]
        [HttpGet(Router.AuthorizationRoute.ManageUserRolesData)]
        public async Task<IActionResult> ManageUserRolesData([FromRoute] int userid)
        {
            //var response = await _mediator.Send(Command);
            //var response = await Mediator.Send(new ManageUserRolesDataQuery(userid)); // calss with ctor
            var response = await Mediator.Send(new ManageUserRolesDataQuery() { UserId = userid }); // calss with out ctor
            return NewResult(response);
        }
        #endregion
    }
}
