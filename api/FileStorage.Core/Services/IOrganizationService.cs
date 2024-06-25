using FileStorage.Core.DTO.Organization;

namespace FileStorage.Core.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDto>> GetAllAsync();
        Task CreateOrganizationAsync(CreateOrganizationDto model);
    }
}
