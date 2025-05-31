using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHealthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }


        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _service.GetAllDepartments();
            return Ok(result);
        }


        [HttpPost("CreateDepartments")]
        public async Task<IActionResult> CreateDepartments(DepartmentDetails model)
        {
            var result = await _service.CreateDepartments(model);
            return Ok(result);
        }


        [HttpPut("UpdateDepartments")]
        public async Task<IActionResult> UpdateDepartments(DepartmentDetails model)
        {
            var result = await _service.UpdateDepartments(model);
            return Ok(result);
        }

        [HttpDelete("DeleteDepartments")]
        public async Task<IActionResult> DeleteDepartments(int id)
        {
            var result = await _service.DeleteDepartments(id);
            return Ok(result);
        }

    }
}
