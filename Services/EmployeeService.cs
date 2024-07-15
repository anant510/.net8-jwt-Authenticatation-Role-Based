using thirdProject.context;
using thirdProject.Interfaces;
using thirdProject.Models;

namespace thirdProject.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly FirstContext _context;

        public EmployeeService(FirstContext context)
        {
            _context = context;
        }
        public Employee AddEmployee(Employee employee)
        {
            var emp = _context.Employees.Add(employee);
            _context.SaveChanges();
            return emp.Entity;
        }

        public List<Employee> GetEmployeeDetails()
        {
            var employees = _context.Employees.ToList();
            return employees;
        }
    }
}
