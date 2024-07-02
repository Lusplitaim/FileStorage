using FileStorage.Core.Data.Entities.MongoDB;
using FileStorage.Core.Data.Storages;
using FileStorage.Core.DTO.File;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.Infrastructure.Data.Storages
{
    public class FileStorage : IFileStorage
    {
        public async Task<List<FileMetadata>> GetAsync(int orgId)
        {
            var fileBucket = GetFileBucket();

            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(info => info.Metadata[nameof(StoredFileMetadata.OrganizationId)], orgId);
            var result = await fileBucket.Find(filter).ToListAsync();

            return result.Select(f => new FileMetadata
            {
                Id = f.Id.ToString(),
                Name = f.Filename,
                Size = f.Length,
                OrganizationId = orgId,
            }).ToList();
        }

        public async Task<FileMetadata?> GetMetadataAsync(string fileId)
        {
            var fileBucket = GetFileBucket();

            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(info => info.Id, ObjectId.Parse(fileId));
            var fileInfo = (await fileBucket.FindAsync(filter)).SingleOrDefault();

            if (fileInfo is null)
            {
                return default;
            }

            FileMetadata result = new()
            {
                Id = fileInfo.Id.ToString(),
                Name = fileInfo.Filename,
                Size = fileInfo.Length,
                OrganizationId = fileInfo.Metadata[nameof(StoredFileMetadata.OrganizationId)].ToInt32(),
            };
            return result;
        }

        public async Task<string> UploadAsync(IFormFile file, int orgId)
        {
            var fileBucket = GetFileBucket();
            using var fs =  file.OpenReadStream();

            var metadata = new StoredFileMetadata
            {
                OrganizationId = orgId,
                ContentType = file.ContentType,
            };
            var uploadOptions = new GridFSUploadOptions();
            uploadOptions.Metadata = metadata.ToBsonDocument();

            var fileId = await fileBucket.UploadFromStreamAsync(file.FileName, fs, uploadOptions);
            return fileId.ToString();
        }

        public async Task EditMetadataAsync(string fileId, EditFileDto model)
        {
            var fileBucket = GetFileBucket();
            await fileBucket.RenameAsync(ObjectId.Parse(fileId), model.Name);
        }

        private IGridFSBucket GetFileBucket()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("filestorage");
            return new GridFSBucket(db);
        }

        public async Task DeleteAsync(string fileId)
        {
            var fileBucket = GetFileBucket();
            await fileBucket.DeleteAsync(ObjectId.Parse(fileId));
        }

        public async Task<DownloadFileDto> GetAsync(string fileId)
        {
            var fileBucket = GetFileBucket();

            var downloadStream = await fileBucket.OpenDownloadStreamAsync(ObjectId.Parse(fileId));
            var metadata = BsonSerializer.Deserialize<StoredFileMetadata>(downloadStream.FileInfo.Metadata);
            var result = new DownloadFileDto
            {
                Name = downloadStream.FileInfo.Filename,
                ContentType = metadata.ContentType,
                FileStream = downloadStream,
            };

            return result;
        }
    }
}
