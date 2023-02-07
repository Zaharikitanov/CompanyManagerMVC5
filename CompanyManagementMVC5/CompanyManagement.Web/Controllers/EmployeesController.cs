using CompanyManagement.Data.Models;
using CompanyManagement.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CompanyManagement.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IOfficeService _officeService;
        private readonly string _serverPath = "~/images/profile_pictures";

        public EmployeesController(IEmployeeService service, IOfficeService officeService )
        {
            _employeeService = service;
            _officeService = officeService;
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var model = await _employeeService.GetAll();
            return View(model);
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _employeeService.GetOne(id);
            if (model == null)
            {
                return View("NotFound");
            }
            ViewBag.ImagePath = $"{_serverPath}/{model.ProfileImage}";
            return View(model);
        }

        // GET: Employees/Create
        public async Task<ActionResult> Create(Guid officeId, Guid companyId)
        {
            ViewBag.OfficeId = officeId;
            ViewBag.CompanyId = companyId;
            ViewBag.OfficesList = await _officeService.GetAllByCompanyId(companyId);
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee, HttpPostedFileBase profileImage, Guid officeId, Guid companyId)
        {
            try
            {
                employee.Id = Guid.NewGuid();
                employee.CompanyId = companyId;
                employee.OfficeId = officeId;
                employee.ProfileImage = UploadProfileImage(profileImage, employee);

                await _employeeService.Create(employee);

                return RedirectToAction("Details", "Offices", new { id = officeId });
            }
            catch(Exception e) 
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, Guid companyId)
        {
            var model = await _employeeService.GetOne(id);
            ViewBag.CompanyId = companyId;
            ViewBag.OfficesList = await _officeService.GetAllByCompanyId(companyId);
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee, HttpPostedFileBase profileImage)
        {
            try
            {
                employee.ProfileImage = UploadProfileImage(profileImage, employee);

                var result = await _employeeService.Update(employee);
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
            var model = await _employeeService.GetOne(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Guid officeId, FormCollection form)
        {
            var files = Directory.GetFiles(Server.MapPath(_serverPath));
            var profileImagePath = string.Empty;

            foreach (var file in files)
            {
                if (file.Contains($"{id}"))
                {
                    profileImagePath = file;
                }
            }
            
            await _employeeService.Delete(id, profileImagePath);
            return RedirectToAction("Details", "Offices", new { id = officeId });
        }

        private string UploadProfileImage(HttpPostedFileBase profileImage, Employee employee)
        {
            var imageName = string.Empty;
            if (profileImage != null && profileImage.ContentLength > 0)
            {
                var fileExtension = Path.GetExtension(profileImage.FileName);
                var fileName = $"{employee.Id}_profileImage{fileExtension}";

                var path = Path.Combine(Server.MapPath(_serverPath), fileName);
                imageName = fileName;
                profileImage.SaveAs(path);
            }

            return imageName;
        }
    }
}
