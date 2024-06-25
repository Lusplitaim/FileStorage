using FileStorage.Core.Data.Entities.MongoDB;
using FileStorage.Core.DTO.File;
using Microsoft.AspNetCore.Http;

namespace FileStorage.Core.Data.Storages
{
    public interface IFileStorage
    {
        Task<List<FileMetadata>> GetAsync(int orgId);
        Task EditMetadataAsync(string fileId, EditFileDto model);
        Task<string> UploadAsync(IFormFile file, int orgId);
        Task DeleteAsync(string fileId);
        Task<DownloadFileDto> GetAsync(string fileId);
    }
}
