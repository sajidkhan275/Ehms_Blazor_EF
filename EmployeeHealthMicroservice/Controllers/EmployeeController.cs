using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain;
using EmployeeHealthMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHealthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDetailsData model)
        {
            var result = await _service.CreateEmployeeAsync(model);
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDetailsData model)
        {
            var result = await _service.UpdateEmployeeAsync(model);
            return Ok(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _service.GetAllEmployeesAsync();
            return Ok(result);
        }


        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _service.DeleteEmployeeAsync(id);
            return Ok(result);
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(EmployeeRoleData role)
        {
            int res = await _service.AddRoleAsync(role);
            return Ok(res);
        }

        [HttpGet("GetRoleEmpWise")]
        public async Task<IActionResult> GetRoleEmpWise(string entraId)
        {
            EmployeeWithRoleDetails employees = (await _service.GetRoleEmpWiseAsync(entraId));
            return Ok(employees);
        }

        [HttpPost("DeletRoleEmpWise")]
        public async Task<IActionResult> DeletRoleEmpWise(EmployeeRoleData role)
        {
            int res = await _service.DeletRoleEmpWiseAsync(role);
            return Ok(res);
        }

        [HttpGet("GetEmpRole")]
        public async Task<IActionResult> GetEmpRole()
        {
            List<EmployeeRoleData> employees = (await _service.GetEmpRole()).ToList();
            return Ok(employees);
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            List<EmployeeRoleData> employees = (await _service.GetAllRole()).ToList();
            return Ok(employees);
        }
    }
}
