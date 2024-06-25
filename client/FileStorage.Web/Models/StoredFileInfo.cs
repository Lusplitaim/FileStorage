namespace FileStorage.Web.Models
{
    public class StoredFileInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int Size { get; set; }
    }
}
