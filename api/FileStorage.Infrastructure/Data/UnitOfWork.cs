using FileStorage.Core.Data;
using FileStorage.Core.Data.Repositories;
using FileStorage.Infrastructure.Data.Repositories;
using FileStorage.Infrastructure.Migrations;

namespace FileStorage.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public IOrganizationRepository OrganizationRepository => new OrganizationRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }
    }
}
