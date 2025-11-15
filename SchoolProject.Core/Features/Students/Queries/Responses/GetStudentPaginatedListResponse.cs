using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Responses
{
    public class GetStudentPaginatedListResponse
    {
        //// not required when Pagination with Mapping
        //public GetStudentPaginatedListResponse(int studID, string? name, string? address, string? deparmentName)
        //{
        //    StudID = studID;
        //    Name = name;
        //    Address = address;
        //    DeparmentName = deparmentName;
        //}

        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? DeparmentName { get; set; }
    }
   
}
