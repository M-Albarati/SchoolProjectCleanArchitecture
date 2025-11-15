using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase //{ControllerBase}  handle ObjectResult ReturnCode = StatusCode
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

        [HttpGet(Router.DepartmentRoute.GetById)]
        public async Task<IActionResult> GetDepartmentById(int id/*[FromRoute]int id*/)
        {
            var response = await Mediator.Send(new GetDepartmentByIdQuery(id));
            return NewResult(response);

        }
        #endregion
    }
}
