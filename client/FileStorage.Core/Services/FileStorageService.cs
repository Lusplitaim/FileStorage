using FileStorage.Core.Models;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace FileStorage.Core.Services
{
    public class FileStorageService : IFileStorageService
    {
        public async Task<IEnumerable<StoredFileInfo>> GetFilesInfo(int orgId)
        {
            try
            {
                var options = new RestClientOptions("https://localhost:7113/api");
                var client = new RestClient(options);

                var request = new RestRequest($"organizations/{orgId}/files/metadata");

                var result = await client.GetAsync<List<StoredFileInfo>>(request) ?? [];
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get organization files", ex);
            }
        }

        public async Task<StoredFileInfo> UploadFile(IFormFile file, int orgId)
        {
            try
            {
                var options = new RestClientOptions("https://localhost:7113/api");
                var client = new RestClient(options);

                var request = new RestRequest($"organizations/{orgId}/files", method: Method.Post);
                request.AddFile("file", file.OpenReadStream, file.FileName, file.ContentType);

                var result = await client.PostAsync<StoredFileInfo>(request);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload a file", ex);
            }
        }

        public async Task DeleteFile(string fileId, int orgId)
        {
            try
            {
                var options = new RestClientOptions("https://localhost:7113/api");
                var client = new RestClient(options);

                var request = new RestRequest($"organizations/{orgId}/files/{fileId}", method: Method.Delete);

                RestResponse result = await client.DeleteAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete a file", ex);
            }
        }
    }
}
