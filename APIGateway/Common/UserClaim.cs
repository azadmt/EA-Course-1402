using Identity.Contract;

namespace APIGateway
{
    public class UserClaim
    {
        public UserClaim()
        {
        }

        private SecurityContext _securityContext;

        public void Set(SecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        public bool HassPermission(string permissionName)
        {
            return _securityContext.Permissions.Contains(permissionName);
        }
    }
}