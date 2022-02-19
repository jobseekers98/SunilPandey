using EmployeeCrudOperation.Models;
using EmployeeCrudOperation.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Repository
{
    public class EmployeeService:IEmployee
    {
        private readonly ApplicationContext context;

        public EmployeeService(ApplicationContext _context)
        {
            context = _context;
        }
        public Employee CreateEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            context.Employees.Remove(emp);
            context.SaveChanges();
            return emp;
        }

        public Employee GetEmployeeById(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee UpdateEmployee(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            context.Employees.Update(emp);
            context.SaveChanges();
            return emp;
        }
    }
}
