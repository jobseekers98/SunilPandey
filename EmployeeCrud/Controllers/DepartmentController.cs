using EmployeeCrudOperation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly ApplicationContext context;

        public DepartmentController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult ShowDepartment()
        {
            List<Department> departments = context.Departments.ToList();
            return View(departments);
        }

        public IActionResult CreateDeapartment()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateDeapartment(Department department)
        {
            //var depts = context.Departments.Find();
            var dept = new Department()
            {
                Depart=department.Depart
            };
            context.Departments.Add(dept);
            context.SaveChanges();
            return RedirectToAction("ShowDepartment");
        }

        public IActionResult DeleteDepartment(int id)
        {
            var dept = context.Departments.SingleOrDefault(e => e.Id == id);
            context.Departments.Remove(dept);
            context.SaveChanges();
            return RedirectToAction("ShowDepartment");

            //if (context.Employees.SingleOrDefault(e => e.Department == dept){
            //    TempData["error"] = "Department can't Delete because because it's taken by someone Employee";
            //    return View("ShowDepartment");
            //}
            //else
            //{
            //    context.Departments.Remove(dept);
            //    context.SaveChanges();
            //    return RedirectToAction("ShowDepartment");
            //}

        }

        public IActionResult UpdateDepartment(int id)
        {
            var dept = context.Departments.SingleOrDefault(e => e.Id == id);
            var d = new Department()
            {
                Depart = dept.Depart
            };
            return View(d);
        }

        [HttpPost]
        public IActionResult UpdateDepartment(Department department)
        {
            var depts = new Department()
            {
                Id=department.Id,
                Depart=department.Depart
            };
            context.Departments.Update(depts);
            context.SaveChanges();
            return RedirectToAction("ShowDepartment");
        }
    }
}
