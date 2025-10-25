using AutoMapper;
using Azure.Core;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    internal class StudentCommandHandler : ResponseHandler,
                                           IRequestHandler<AddStudentCommand, Response<string>>,
                                           IRequestHandler<EditStudentCommand, Response<string>>,
                                           IRequestHandler<DeleteStudentCommand, Response<string>>
    {

        #region Feilds
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // check Null
            if (request == null) { return BadRequest<string>(); }
            
            // Mapping Between Requst and Student
            Student StudentMapper = _mapper.Map<Student>(request);
            var reslt = await _studentService.AddAsync(StudentMapper);
            
            // check Exist
           // if (reslt == "Exist") { return UnprocessableEntity<string>("Exist"); }
            // check Exception
            //if (reslt == "Exception") { return UnprocessableEntity<string>("Exception Error"); }
            // check Success
            if (reslt == "Success") { return Added<string>("Student"); }
            // check Bad Request
            else { return BadRequest<string>(); }

            
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // check Null
            if (request == null) { return BadRequest<string>(); }

            // check Exist
            var student = await _studentService.GetStudentByIdAsync(request.StudID);
            if (student == null) { return NotFound<string>(); }

            // Mapping Between Requst and Student
            //var StudentMapper = _mapper.Map<Student>(request);
            var StudentMapper = _mapper.Map(request, student);
            var reslt = await _studentService.EditAsync(StudentMapper);

            // check Exception
            //if (reslt == "Exception") { return UnprocessableEntity<string>("Exception Error"); }
            // check Success
            if (reslt == "Success") { return Updated<string>("Student"); }
            // check Bad Request
            else { return BadRequest<string>(); }
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // check Null
            if (request == null) { return BadRequest<string>(); }

            // Mapping Between Requst and Student
            Student StudentMapper = _mapper.Map<Student>(request);
            var reslt = await _studentService.DeleteAsync(StudentMapper);

            // check Exist
            // if (reslt == "Exist") { return UnprocessableEntity<string>("Exist"); }
            // check Exception
            //if (reslt == "Exception") { return UnprocessableEntity<string>("Exception Error"); }
            // check Success
            if (reslt == "Success") { return Deleted<string>(); }
            // check Bad Request
            else { return BadRequest<string>(); }
        }
        #endregion

    }
}
