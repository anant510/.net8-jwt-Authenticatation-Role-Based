using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thirdProject.context;
using thirdProject.Models;

namespace thirdProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly FirstContext _context;

        public EmployeeController(FirstContext context)
        {
            _context = context;
        }

        //Getting All the Data form Employee Table, GET: api/Employee

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeDetails()
        {
            return await _context.Employees.ToListAsync();
        }

        //Data Added in Employee Table, POST: api/Employee

        [HttpPost]

        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok("sucessfully posted");
        }

        //Getting each data by using id, GET: api/Employee/1

        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>> GetEmployeeByID(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        //Deleting each data using id: DELETE: api/Employee/5

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok("Deleted Sucessfully");
        }


        // Updating the data uisng id, PUT: api/Employee/1

        [HttpPut("{id}")]

        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Update the existing employee's properties with the new values
            existingEmployee.Name = employee.Name;
            existingEmployee.Description = employee.Description;

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the employee still exists in the database
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Re-throw the caught exception to propagate it up the call stack
                    throw;
                }
            }

            return Ok("Employee Updated Sucessfully");

        }

        // Helper method to check if an employee exists in the database
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }


    }
}
