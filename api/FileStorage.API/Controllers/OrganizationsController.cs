using FileStorage.Core.DTO.Organization;
using FileStorage.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationService m_OrganizationService;
        public OrganizationsController(IOrganizationService organizationService)
        {
            m_OrganizationService = organizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetOrganizationsAsync()
        {
            var result = await m_OrganizationService.GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrganizationAsync(CreateOrganizationDto model)
        {
            await m_OrganizationService.CreateOrganizationAsync(model);

            return Created();
        }
    }
}
