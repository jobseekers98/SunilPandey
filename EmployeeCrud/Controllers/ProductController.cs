using Microsoft.AspNetCore.Hosting;
using EmployeeCrudOperation.Models;
using EmployeeCrudOperation.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeCrudOperation.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationContext context;
        private readonly IHostingEnvironment environment;

        public ProductController(ApplicationContext _context,IHostingEnvironment environment )
        {
            context = _context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            var path = environment.WebRootPath;
            var filePath = "Content/Images/" + model.ProductImage.FileName;
            var fullPath = Path.Combine(path, filePath);
            UploadFile(model.ProductImage, fullPath);
            var result = new Product()
            {
                ProductName=model.ProductName,
                ProductImage=filePath,
            };
            context.Products.Add(result);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var prd = context.Products.SingleOrDefault(e => e.Id == id);
            var pr = new ProductCreateViewModel()
            {
                ProductName = prd.ProductName,
                OldImage=prd.ProductImage
            };
            return View(pr);
        }
        [HttpPost]
        public IActionResult Edit(ProductCreateViewModel model)
        {

            var path = environment.WebRootPath;
            var filePath = "Content/Images/" + model.ProductImage.FileName;
            var fullPath = Path.Combine(path, filePath);
            UploadFile(model.ProductImage, fullPath);
            var result = new Product()
            {
                Id=model.Id,
                ProductName = model.ProductName,
                ProductImage = filePath,
            };
            context.Products.Update(result);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var result = context.Products.SingleOrDefault(e => e.Id == id);
            context.Products.Remove(result);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var det = context.Products.SingleOrDefault(e => e.Id == id);
            var details = new ProductCreateViewModel()
            {
                Id=det.Id,
                ProductName=det.ProductName,
                OldImage=det.ProductImage
            };
            return View(details);
        }

    }
}
