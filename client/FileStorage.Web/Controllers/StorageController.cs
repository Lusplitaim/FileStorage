using FileStorage.Web.Models;
using FileStorage.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Web.Controllers
{
    [Route("files")]
    public class StorageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new StorageViewModel();
            viewModel.StoredFiles.AddRange(GetFiles());
            return View(viewModel);
        }

        [HttpGet("{id:int}")]
        public IActionResult FileInfo(int id)
        {
            var files = GetFiles();
            var fileInfo = files.SingleOrDefault(f => f.Id == id);
            if (fileInfo is null)
            {
                return NotFound();
            }

            FileInfoViewModel viewModel = new()
            {
                Id = fileInfo.Id,
                Name = fileInfo.Name,
                Created = fileInfo.Created,
                Size = fileInfo.Size,
            };
            return View(viewModel);
        }

        [HttpGet("create-file")]
        public IActionResult CreateFile()
        {
            return View();
        }

        private List<StoredFileInfo> GetFiles()
        {
            List<StoredFileInfo> result = [];
            result.Add(new StoredFileInfo() { Id = 1, Name = "app.js", Created = DateTime.UtcNow, Size = 15 });
            result.Add(new StoredFileInfo() { Id = 2, Name = "trel.js", Created = DateTime.UtcNow, Size = 2 });
            result.Add(new StoredFileInfo() { Id = 3, Name = "privid.mp4", Created = DateTime.UtcNow, Size = 54 });
            result.Add(new StoredFileInfo() { Id = 4, Name = "rec10.mp3", Created = DateTime.UtcNow, Size = 30 });
            result.Add(new StoredFileInfo() { Id = 5, Name = "plst.txt", Created = DateTime.UtcNow, Size = 1 });

            return result;
        }
    }
}
