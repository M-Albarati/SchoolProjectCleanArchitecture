using AutoMapper;
using MediatR;
using Microsoft.VisualBasic;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
                                  IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>,
                                  IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                  IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
                                 



    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Function
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {

            var studentList = await _studentService.GetStudentsListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);
            return Success(studentListMapper);
            // return await _studentService.GetStudentsListAsync();

        }

        public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
                var student = await _studentService.GetStudentByIdAsync(request.Id);
                if (student == null)
                {
                return NotFound<GetStudentResponse>(); 
                }
                var studentMapper = _mapper.Map<GetStudentResponse>(student);
                return Success(studentMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            // Pagination with Expression
            //Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudID, e.Name, e.Address, e.Department.DName);
            //var queryable = _studentService.FilterStudentsPaginatedQueryable(request.Search,request.OrderBy);
            //var PaginatedList = await queryable.Select(expression).ToPaginatedListAsync(request.PageNumber,request.PageSize);

            // Pagination with query
            var queryable = _studentService.FilterStudentsPaginatedQueryable(request.Search, request.OrderBy);
            var PaginatedList = await queryable.Select(x=> new GetStudentPaginatedListResponse(x.StudID,x.Name,x.Address,x.Department.DName) ).ToPaginatedListAsync(request.PageNumber,request.PageSize);

            return PaginatedList;
        }




        #endregion

    }
}
