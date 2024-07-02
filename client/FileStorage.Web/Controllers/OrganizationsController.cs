using FileStorage.Core.Services;
using FileStorage.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FileStorage.Web.Controllers
{
    [Route("organizations")]
    [Route("")]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationService m_OrganizationService;
        public OrganizationsController(IOrganizationService orgService)
        {
            m_OrganizationService = orgService;
        }

        [HttpGet]
        public async Task<IActionResult> Organizations()
        {
            var orgs = await m_OrganizationService.GetAsync();
            List<OrganizationViewModel> orgsViewModel = orgs.Select(o => new OrganizationViewModel { Id = o.Id, Name = o.Name }).ToList();

            ViewBag.Organizations = new SelectList(orgsViewModel, nameof(OrganizationViewModel.Id), nameof(OrganizationViewModel.Name), null);

            ViewData["Title"] = "Available organizations";

            return View();
        }
    }
}
