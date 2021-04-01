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
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            this._service = service;
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var model = await _service.GetAll();
            return View(model);
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _service.GetOne(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee, Guid officeId, FormCollection collection)
        {
            try
            {
                var result = await _service.Create(employee, officeId);
                return RedirectToAction("Details", "Companies", new { id = officeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
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

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee)
        {
            try
            {
                var result = await _service.Update(employee);
                return RedirectToAction("Details", new { id = result.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
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

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, FormCollection form)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
