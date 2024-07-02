using FileStorage.Core.Models;
using RestSharp;

namespace FileStorage.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        public async Task<List<Organization>> GetAsync()
        {
            try
            {
                var options = new RestClientOptions("https://localhost:7113/api");
                var client = new RestClient(options);

                var request = new RestRequest("Organizations");

                var result = await client.GetAsync<List<Organization>>(request) ?? [];
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get organizations", ex);
            }
        }
    }
}
