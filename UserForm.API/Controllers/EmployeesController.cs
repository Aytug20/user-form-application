using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserForm.API.Data;
using UserForm.API.Models;

namespace UserForm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeesController : Controller
    {
        private readonly UserFormDbContext _userFormDbContext;

        public EmployeesController(UserFormDbContext userFormDbContext)
        {
            _userFormDbContext = userFormDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _userFormDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _userFormDbContext.Employees.AddAsync(employeeRequest);
            await _userFormDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            var employee = 
                await _userFormDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _userFormDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Lastname = updateEmployeeRequest.Lastname;
            employee.Birthdate = updateEmployeeRequest.Birthdate;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;

            await _userFormDbContext.SaveChangesAsync();

            return Ok(employee);


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _userFormDbContext.Employees.FindAsync(id);

            if (employee == null )
            {
                return NotFound();
            }

            _userFormDbContext.Employees.Remove(employee);
            await _userFormDbContext.SaveChangesAsync();

            return Ok(employee);
        }



    }
    }

