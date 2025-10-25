using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public DepartmentQueryHandler(IDepartmentService departmentService,IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }




        #endregion

        #region Handel Function
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            // Service GetById including students, subjects ,instructors
            var Response = await _departmentService.GetDepartmentByIdAsync(request.Id);

            // Check Is Not Exist
            if (Response == null) { return NotFound<GetDepartmentByIdResponse>(); }

            // Mapping
            var departmentMapper = _mapper.Map<GetDepartmentByIdResponse>(Response);
            // Return the Result
            return Success(departmentMapper);
        }

        #endregion

    }
}
