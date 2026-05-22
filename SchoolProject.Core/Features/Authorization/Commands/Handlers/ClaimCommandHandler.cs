using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimCommandHandler: ResponseHandler,
                                      IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractor
        public ClaimCommandHandler(IAuthorizationService authorizationService,
                                           IMapper mapper)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public  async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaimsAsync(request);
            switch (result)
            {
                case "User Not Found": return NotFound<string>("User Not Found");
                case "Faild Remove Old Claims": return BadRequest<string>("Faild Remove Old Claims");
                case "Faild Add New Claims": return BadRequest<string>("Faild Add New Claims");
                case "Faild Update User Claims": return BadRequest<string>("Faild Update User Claims");
            }
            return Updated<string>("");
        }
        #endregion
    }
}
