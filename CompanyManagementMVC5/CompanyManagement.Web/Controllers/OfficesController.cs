using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CompanyManagement.Web.Controllers
{
    public class OfficesController : Controller
    {
        private readonly IOfficeService _service;

        public OfficesController(IOfficeService service)
        {
            _service = service;
        }
        // GET: Offices for company
        public async Task<ActionResult> Index()
        {
            var model = await _service.GetAll();
            return View(model);
        }

        // GET: Offices/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _service.GetOne(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        // GET: Offices/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.CompanyId = id;
            return View();
        }

        // POST: Offices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Office office, Guid id, FormCollection collection)
        {
            try
            {
                var result = await _service.Create(office, id);
                return RedirectToAction("Details", "Companies", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Offices/Edit/5
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

        // POST: Offices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Office office)
        {
            try
            {
                var result = await _service.Update(office);
                return RedirectToAction("Details", new { id = result.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Offices/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var model = await _service.GetOne(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        // POST: Offices/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, FormCollection form)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
