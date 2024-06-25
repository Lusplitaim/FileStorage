using FileStorage.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FileStorage.Web.Controllers
{
    [Route("organizations")]
    [Route("")]
    public class OrganizationsController : Controller
    {
        [HttpGet]
        public IActionResult Organizations()
        {
            List<OrganizationViewModel> orgs = [];
            orgs.Add(new() { Id = 1, Name = "Google" });
            orgs.Add(new() { Id = 2, Name = "IBM" });
            orgs.Add(new() { Id = 3, Name = "Pachunko" });
            orgs.Add(new() { Id = 4, Name = "Levi" });

            ViewBag.Organizations = new SelectList(orgs, nameof(OrganizationViewModel.Id), nameof(OrganizationViewModel.Name));

            ViewData["Title"] = "Available organizations";

            return View(orgs);
        }
    }
}
