using FileStorage.Core.Models;

namespace FileStorage.Core.Services
{
    public interface IOrganizationService
    {
        Task<List<Organization>> GetAsync();
    }
}
