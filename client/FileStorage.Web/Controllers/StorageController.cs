using FileStorage.Core.Models;
using FileStorage.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Web.Controllers
{
    [Route("Files")]
    public class StorageController : Controller
    {
        private readonly IFileStorageService m_FileStorageService;
        public StorageController(IFileStorageService fileStorageService)
        {
            m_FileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int orgId)
        {
            var model = await m_FileStorageService.GetFilesInfo(orgId);
            return PartialView(model);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, int orgId)
        {
            var result = await m_FileStorageService.UploadFile(file, orgId);
            return PartialView("Index", new List<StoredFileInfo>{ result });
        }

        [HttpDelete("DeleteFile/{fileId}")]
        public async Task<IActionResult> DeleteFile(string fileId, int orgId)
        {
            await m_FileStorageService.DeleteFile(fileId, orgId);
            return Ok();
        }
    }
}
