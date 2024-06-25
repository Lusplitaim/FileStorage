namespace FileStorage.Core.DTO.File
{
    public class DownloadFileDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Stream FileStream { get; set; }
    }
}
