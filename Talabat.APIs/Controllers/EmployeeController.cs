using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class EmployeeController : BaseAPIsController
    {
        private readonly IGenericRepository<Employee> _employeeRepo;

        public EmployeeController(IGenericRepository<Employee> EmployeeRepo)
        {

            _employeeRepo = EmployeeRepo;

        }
        [CachedAttribute(600)]
        [HttpGet] // Get : api/employee
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSpecifiaction();

            var employee = await _employeeRepo.GetallwithSpecAsync(spec);

            return Ok(employee);

        }
        [HttpGet("{id}")] // Get : api/employee
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifiaction(id);

            var employee = await _employeeRepo.GetIdwithSpecAsync(spec);

            return Ok(employee);

        }
    }
}
