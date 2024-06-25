using Identity.Contract;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Controllers
{
    public static class InMemoryDB
    {
        public static List<User> Users = new List<User>();
        public static Dictionary<string, SecurityContext> Tokens = new Dictionary<string, SecurityContext>();
        public static Dictionary<string, string[]> UserPermissions = new Dictionary<string, string[]>();
        public static Dictionary<string, string[]> RolePermissions = new Dictionary<string, string[]>();

        static InMemoryDB()
        {
            Users.Add(new User { Id = 1, Username = "admin", Password = "123", EmailAddress = "admin@mail.com", Roles = new[] { "admin", "customer" } });
            Users.Add(new User { Id = 2, Username = "c1", Password = "123", EmailAddress = "c1@mail.com", Roles = new[] { "customer" } });
            Users.Add(new User { Id = 2, Username = "c2", Password = "123", EmailAddress = "c2@mail.com", Roles = new[] { "customer" } });
            UserPermissions.Add("admin", new[] { "RegisterOrder" });

            RolePermissions.Add("admin", new[] { "CreateProductCategory", "CreateProduct", "EditProduct", "RejectOrder", "EditPaymment" });
            RolePermissions.Add("customer", new[] { "RegisterOrder", "EditOrder" });
        }
    }

    public static class PermissionManager
    {
        public static string[] GetUserPermissions(User user)
        {
            var userPermissions = new List<string>();
            userPermissions.AddRange(GetUserPermissions(user.Username));
            userPermissions.AddRange(GetRolePermissions(user.Roles));
            return userPermissions.Distinct().ToArray();
        }

        public static string[] GetUserPermissions(string userName)
        {
            InMemoryDB.UserPermissions.TryGetValue(userName, out string[] permissions);
            return permissions ?? new string[] { };
        }

        public static string[] GetRolePermissions(string[] roles)
        {
            var rolePermissions = new List<string>();
            foreach (var role in roles)
            {
                InMemoryDB.RolePermissions.TryGetValue(role, out string[] permissions);
                if (permissions != null)
                    rolePermissions.AddRange(permissions);
            }

            return rolePermissions.ToArray();
        }
    }
}