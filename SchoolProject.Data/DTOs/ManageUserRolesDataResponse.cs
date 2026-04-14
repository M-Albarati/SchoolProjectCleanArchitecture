using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.DTOs

{
    public class ManageUserRolesDataResponse
    {
        public int UserId { get; set; }
        public List<Roles> RoleList { get; set; }

        public class Roles
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasRole { get; set; }
        }
    }
}
