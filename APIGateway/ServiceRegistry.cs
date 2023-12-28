using APIGateway.Model;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;
using System.Net;

namespace APIGateway
{
    public static class ServiceRegistry
    {
        private static Dictionary<string, List<ServiceRegistryModel>> _services = new();

        public static void RegisterNewServcie(ServiceRegistryModel model)
        {
            if (_services.ContainsKey(model.Name))
            {
                _services[model.Name].Add(model);
            }
            else
            {
                var list = new List<ServiceRegistryModel>
                {
                    model
                };
                _services.Add(model.Name, list);
            }
        }

        public static string GetService(string name)
        {
            _services.TryGetValue(name, out var service);
            var healthyService = service?.FirstOrDefault(p => p.Healthy);
            if (healthyService is null)
                throw new ServiceUnHealthyException($"Not found healthy service of type {name}");
            return healthyService.Url;
        }

        public static HealthCheckResult CheckHealth(string serviceUrl)
        {
            var client = new RestClient($"{serviceUrl}/hc");
            var request = new RestRequest();

            var res = client.Get(request);

            return res.StatusCode == HttpStatusCode.OK ?
               new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "The API is healthy （。＾▽＾）") :
              new HealthCheckResult(
                      status: HealthStatus.Unhealthy,
                      description: "The API is sick (‘﹏*๑)");
        }
    }

    public class ServiceDiscovery
    {
    }

    public class ServiceRegistryModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Healthy { get; set; }
    }
}