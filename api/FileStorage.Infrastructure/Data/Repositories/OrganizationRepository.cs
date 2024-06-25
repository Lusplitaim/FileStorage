using FileStorage.Core.Data.Entities;
using FileStorage.Core.Data.Repositories;
using FileStorage.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private DatabaseContext m_DbContext;
        public OrganizationRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task CreateAsync(Organization organization)
        {
            await m_DbContext.Organizations.AddAsync(organization);
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await m_DbContext.Organizations.ToListAsync();
        }
    }
}
