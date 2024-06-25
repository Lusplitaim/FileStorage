using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FileStorage.Core.Data.Entities.MongoDB
{
    public class FileMetadata
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
    }
}
