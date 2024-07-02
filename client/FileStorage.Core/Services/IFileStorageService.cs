using FileStorage.Core.Models;
using Microsoft.AspNetCore.Http;

namespace FileStorage.Core.Services
{
    public interface IFileStorageService
    {
        Task<IEnumerable<StoredFileInfo>> GetFilesInfo(int orgId);
        Task<StoredFileInfo> UploadFile(IFormFile file, int orgId);
        Task DeleteFile(string fileId, int orgId);
    }
}
