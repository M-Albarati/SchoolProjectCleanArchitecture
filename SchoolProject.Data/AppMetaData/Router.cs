using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        private const string Root = "Api";
        private const string Version = "V1";
        private const string Rule = Root+ "/" + Version;

        public static class StudentRoute
        {
            private const string Prefix = Rule + "/" + "Student";
            public const string List = Prefix + "/" + "List";
            public const string GetById = Prefix + "/" + "{id}";
            public const string Paginated = Prefix + "/" + "Paginated";

            public const string Create = Prefix + "/" + "Create";
            public const string Update = Prefix + "/" + "Update";
            public const string Delete = Prefix + "/" + "Delete";
        }
        public static class DepartmentRoute
        {
            private const string Prefix = Rule + "/" + "Department";
            public const string List = Prefix + "/" + "List";
            public const string GetById = Prefix + "/" + "{id}";
            public const string Paginated = Prefix + "/" + "Paginated";

            public const string Create = Prefix + "/" + "Create";
            public const string Update = Prefix + "/" + "Update";
            public const string Delete = Prefix + "/" + "Delete";
        }
        public static class UserRoute
        {
            private const string Prefix = Rule + "/" + "User";
            public const string List = Prefix + "/" + "List";
            public const string GetById = Prefix + "/" + "{id}";
            public const string Paginated = Prefix + "/" + "Paginated";

            public const string Create = Prefix + "/" + "Create";
            public const string Update = Prefix + "/" + "Update";
            public const string Delete = Prefix + "/" + "Delete";
            public const string ChangePassword = Prefix + "/" + "ChangePassword";
        }
        public static class AuthRoute
        {
            private const string Prefix = Rule + "/" + "Auth";
            public const string SignIn = Prefix + "/" + "SignIn";
            public const string RefreshToken = Prefix + "/" + "RefreshToken";
            public const string ValidateToken = Prefix + "/" + "ValidateToken";
        }
        public static class AuthorizationRoute
        {
            private const string Prefix = Rule + "/" + "Authorization";
            public const string Create = Prefix + "/" + "Role/Create";
            public const string Update = Prefix + "/" + "Role/Update";
            public const string Delete = Prefix + "/" + "Role/Delete";
        }
    }
}
