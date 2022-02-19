using EmployeeCrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Repository.Contract
{
    public interface IEmployee
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        Employee CreateEmployee(Employee employee);
        Employee DeleteEmployee(int id);
        Employee UpdateEmployee(int id);
    }
}
