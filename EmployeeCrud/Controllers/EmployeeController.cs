using EmployeeCrudOperation.Models;
using EmployeeCrudOperation.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeCrudOperation.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationContext context;
        private readonly IHostingEnvironment environment;

        public EmployeeController(ApplicationContext _context,IHostingEnvironment environment)
        {
            context = _context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var ed = (from e in context.Employees
                      join
                      d in context.Departments
                      on e.Department.Id equals d.Id
                      select new EmployeeDepartmentViewModel()
                      {
                          Id=e.Id,
                          FirstName = e.FirstName,
                          LastName = e.LastName,
                          Gender = e.Gender,
                          Mobile = e.Mobile,
                          Department = d.Depart,

                      }).ToList();
            return View(ed);
        }
        public void UploadImage(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
        public IActionResult Create()
        {
            var depts = context.Departments.ToList();
            ViewBag.depts = depts;
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                int id = model.Department;
                var dept = context.Departments.SingleOrDefault(e => e.Id == id);
                var path = environment.WebRootPath;
                var filePath = "Content/ProfileImage/" + model.ProfileImage.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadImage(model.ProfileImage, fullPath);

                var emp = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Department = dept,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Address = model.Address,
                    Salary = model.Salary,
                    ProfileImage = filePath

                };
                context.Employees.Add(emp);
                context.SaveChanges();
                return RedirectToAction("Index");
            };
                TempData["error"] = "Empty Form Can't Submited!";
                return View(model);
        }

        public IActionResult Delete(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
                TempData["error"] = "Record has been Deleted!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Employee Not Deleted!";
                return View("Index");
            }
        }

        public IActionResult Update(int Id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == Id);
            if (emp != null)
            {
                var depts = context.Departments.ToList();
                ViewBag.depts = depts;
                var upt = new EmployeeCreateModel()
                {
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Department = emp.Department.Id,
                    Address = emp.Address,
                    Email = emp.Email,
                    Gender = emp.Gender,
                    Mobile = emp.Mobile,
                    Salary = emp.Salary
                };
                return View(upt);
            }
            else
            {
                TempData["error"] = "Not Found!";
                return View();
            }
        }
        [HttpPost]
        public IActionResult Update(EmployeeCreateModel model)
        {
            int id = model.Department;
            var dept = context.Departments.SingleOrDefault(e => e.Id == id);
            var emp = new Employee()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Department = dept,
                Address = model.Address,
                Email = model.Email,
                Gender = model.Gender,
                Mobile = model.Mobile,
                Salary = model.Salary
            };
            context.Employees.Update(emp);
            context.SaveChanges();
            TempData["error"] = "Record Updated!";
            return RedirectToAction("Index");
        }
        //All Data fetch from DB 
        public IActionResult Details(int id)
        {
            var ed = (from e in context.Employees.Where(e=>e.Id==id)
                      join
                      d in context.Departments
                      on e.Department.Id equals d.Id
                      select new EmployeeDepartmentViewModel()
                      {
                          Id = e.Id,
                          FirstName = e.FirstName,
                          LastName = e.LastName,
                          Gender = e.Gender,
                          Email = e.Email,
                          Mobile = e.Mobile,
                          Address = e.Address,
                          Department = d.Depart,
                          Salary = e.Salary,
                          Image = e.ProfileImage
                      }).ToList();
            return View(ed);
        }
    }
}