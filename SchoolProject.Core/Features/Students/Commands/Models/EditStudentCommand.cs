using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class EditStudentCommand: IRequest<Response<string>>
    {
        //[Required]
        public int StudID { get; set; }
        public string? Name { get; set; }
        //[Required]
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? DeparmentId { get; set; }
    }
}
