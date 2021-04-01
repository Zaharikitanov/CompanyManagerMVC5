using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CompanyManagement.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _service;

        public CompaniesController(ICompanyService service)
        {
            _service = service;
        }
        // GET: Companies
        public async Task<ActionResult> Index()
        {
            var model = await _service.GetAll();
            return View(model);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _service.GetOne(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Company company)
        {
            if (String.IsNullOrEmpty(company.Name))
            {
                ModelState.AddModelError(nameof(company.Name), "The name is required");
            }

            if (ModelState.IsValid)
            {
                var result = await _service.Create(company);
                return RedirectToAction("Details", new { id = result.Id });
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await _service.GetOne(id);

            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Company company)
        {
            if (String.IsNullOrEmpty(company.Name))
            {
                ModelState.AddModelError(nameof(company.Name), "The name is required");
            }

            if (ModelState.IsValid)
            {
                var result = await _service.Update(company);
                return RedirectToAction("Details", new { id = result.Id });
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var model = await _service.GetOne(id);
            if(model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, FormCollection form)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}