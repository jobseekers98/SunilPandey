using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Models.ViewModel
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string OldImage { get; set; }
        public IFormFile ProductImage { get; set; }
    }
}
