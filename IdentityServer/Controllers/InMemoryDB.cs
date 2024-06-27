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
}