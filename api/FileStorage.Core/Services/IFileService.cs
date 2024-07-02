using FileStorage.Core.Data.Entities.MongoDB;
using FileStorage.Core.DTO.File;
using Microsoft.AspNetCore.Http;

namespace FileStorage.Core.Services
{
    public interface IFileService
    {
        Task<IEnumerable<FileMetadata>> GetFilesMetadataAsync(int orgId);
        Task EditFileMetadataAsync(string fileId, EditFileDto model);
        Task<FileMetadata> UploadFileAsync(IFormFile file, int orgId);
        Task<DownloadFileDto> GetFileAsync(string fileId);
        Task DeleteFileAsync(string fileId);
    }
}
