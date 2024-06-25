using FileStorage.Core.DTO.File;
using FileStorage.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("organizations/{orgId}/files")]
    public class FilesController : BaseController
    {
        private IFileService m_FileService;
        public FilesController(IFileService fileService)
        {
            m_FileService = fileService;
        }

        [HttpGet("metadata")]
        public async Task<ActionResult> GetFilesMetadata(int orgId)
        {
            return Ok(await m_FileService.GetFilesMetadataAsync(orgId));
        }

        [HttpPut("{fileId}/metadata")]
        public async Task<ActionResult> EditFileMetadata(string fileId, EditFileDto model)
        {
            await m_FileService.EditFileMetadataAsync(fileId, model);

            return Ok();
        }

        [HttpGet("{fileId}")]
        public async Task<ActionResult> GetFile(string fileId)
        {
            var fileDto = await m_FileService.GetFileAsync(fileId);

            return File(fileDto.FileStream, fileDto.ContentType, fileDto.Name);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file, int orgId)
        {
            var fileId = await m_FileService.UploadFileAsync(file, orgId);

            return CreatedAtAction(nameof(UploadFile), fileId);
        }

        [HttpDelete("{fileId}")]
        public async Task<ActionResult> DeleteFile(string fileId)
        {
            await m_FileService.DeleteFileAsync(fileId);

            return NoContent();
        }
    }
}
