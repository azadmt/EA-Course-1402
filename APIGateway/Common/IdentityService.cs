using Identity.Contract;
using RestSharp;

namespace APIGateway
{
    public class IdentityService
    {
        public SecurityContext GetSecurityContext(string token)
        {
            //get from config
            var client = new RestClient($"http://localhost:45364/Identity?token={token}");
            var request = new RestRequest();

            var result = client.Get<SecurityContext>(request);
            return result;
        }
    }
}