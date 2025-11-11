using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Handlers;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StudentController : AppControllerBase //{ControllerBase}  handle ObjectResult ReturnCode = StatusCode
    {
        #region Fields
        //private readonly IMediator _mediator;
        #endregion

        #region Constractor
        //public StudentController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}
        #endregion
                        // تم استبدال
                        // ( _mediator => Mediator)
                        // الذي في الكلاس
                        // (AppControllerBase)

        #region Handel Function
        [HttpGet(Router.StudentRoute.List)]
        public async Task<IActionResult> GetStudentList()
        {
            //var response = await _mediator.Send(new GetStudentListQuery());
            var response = await Mediator.Send(new GetStudentListQuery());
            return NewResult(response);
        }

        [HttpGet(Router.StudentRoute.Paginated)]
        public async Task<IActionResult> GetStudentPaginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            //var response = await _mediator.Send(query);
            var response = await Mediator.Send(query);
            return Ok(response);
           
        }

        [HttpGet(Router.StudentRoute.GetById)]
        public async Task<IActionResult> GetStudentById(int id/*[FromRoute]int id*/)
        {
            //var response = await _mediator.Send(new GetStudentByIdQuery(id));
            var response = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(response);

        }
        [HttpPost(Router.StudentRoute.Create)]
        public async Task<IActionResult> Create ([FromBody] AddStudentCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
           return NewResult(response);

        }
        [HttpPost(Router.StudentRoute.Update)]
        public async Task<IActionResult> Update([FromBody] EditStudentCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);

        }

        [HttpPost(Router.StudentRoute.Delete)]
        public async Task<IActionResult> Delete([FromBody] DeleteStudentCommand Command)
        {
            //var response = await _mediator.Send(Command);
            var response = await Mediator.Send(Command);
            return NewResult(response);

        }
        #endregion


    }
}
