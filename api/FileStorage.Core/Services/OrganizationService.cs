using FileStorage.Core.Data;
using FileStorage.Core.Data.Entities;
using FileStorage.Core.DTO.Organization;

namespace FileStorage.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private IUnitOfWork m_UnitOfWork;
        public OrganizationService(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task CreateOrganizationAsync(CreateOrganizationDto model)
        {
            try
            {
                Organization org = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                };
                await m_UnitOfWork.OrganizationRepository.CreateAsync(org);

                await m_UnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create organization", ex);
            }
        }

        public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
        {
            try
            {
                var orgs = await m_UnitOfWork.OrganizationRepository.GetAllAsync();

                return orgs.Select(e => new OrganizationDto { Id = e.Id, Name = e.Name, Description = e.Description });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get organizations", ex);
            }
        }
    }
}
