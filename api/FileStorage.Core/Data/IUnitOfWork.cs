using FileStorage.Core.Data.Repositories;

namespace FileStorage.Core.Data
{
    public interface IUnitOfWork
    {
        IOrganizationRepository OrganizationRepository { get; }

        Task SaveAsync();
    }
}
