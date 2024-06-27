using Identity.Contract;

namespace IdentityServer.Controllers
{
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