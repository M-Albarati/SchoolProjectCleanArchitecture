using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Results
{
    public class ManageUserClaimsDataResponse
    {
        public int UserId { get; set; }
        public List<UserClaim> ClaimList { get; set; }

        public class UserClaim
        {
            public string Type { get; set; }
            public bool Value { get; set; }
        }
    }
}
