using FileStorage.Core.Data.Entities.MongoDB;
using FileStorage.Core.Data.Storages;
using FileStorage.Core.DTO.File;
using Microsoft.AspNetCore.Http;

namespace FileStorage.Core.Services
{
    public class FileService : IFileService
    {
        private IFileStorage m_FileStorage;
        public FileService(IFileStorage fileStorage)
        {
            m_FileStorage = fileStorage;
        }

        public async Task EditFileMetadataAsync(string fileId, EditFileDto model)
        {
            try
            {
                await m_FileStorage.EditMetadataAsync(fileId, model);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get files", ex);
            }
        }

        public async Task<IEnumerable<FileMetadata>> GetFilesMetadataAsync(int orgId)
        {
            try
            {
                return await m_FileStorage.GetAsync(orgId);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to get files", ex);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, int orgId)
        {
            try
            {
                return await m_FileStorage.UploadAsync(file, orgId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload a file", ex);
            }
        }

        public async Task DeleteFileAsync(string fileId)
        {
            try
            {
                await m_FileStorage.DeleteAsync(fileId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete a file", ex);
            }
        }

        public async Task<DownloadFileDto> GetFileAsync(string fileId)
        {
            try
            {
                return await m_FileStorage.GetAsync(fileId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get a file", ex);
            }
        }
    }
}
