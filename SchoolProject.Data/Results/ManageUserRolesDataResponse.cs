using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Results

{
    public class ManageUserRolesDataResponse
    {
        public int UserId { get; set; }
        public List<UserRole> RoleList { get; set; }

        public class UserRole
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasRole { get; set; }
        }
    }
}
