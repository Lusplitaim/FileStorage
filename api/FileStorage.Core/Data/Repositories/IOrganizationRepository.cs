using FileStorage.Core.Data.Entities;

namespace FileStorage.Core.Data.Repositories
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAllAsync();
        Task CreateAsync(Organization organization);
    }
}
