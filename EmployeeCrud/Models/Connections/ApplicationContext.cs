using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrudOperation.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EmployeeCrudOperation.Models.Entities;

namespace EmployeeCrudOperation.Models
{
    public class ApplicationContext:IdentityDbContext<IdentityCustomUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
        
    }
}
