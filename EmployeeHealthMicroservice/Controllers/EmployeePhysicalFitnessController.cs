using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHealthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePhysicalFitnessController : ControllerBase
    {
        private readonly IEmployeePhysicalFitnessService _service;
        public EmployeePhysicalFitnessController(IEmployeePhysicalFitnessService service)
        {
            _service = service;
        }


        [HttpPost("CreateEmployeePhysicalFitness")]
        public async Task<IActionResult> CreateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness model)
        {
            var result = await _service.CreateEmployeePhysicalFitnessAsync(model);
            return Ok(result);
        }

        [HttpGet("GetAllEmployeePhysicalFitness")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllEmployeePhysicalFitness();
            return Ok(result);
        }

        [HttpPut("UpdateEmployeePhysicalFitness")]
        public async Task<IActionResult> UpdateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness model)
        {
            var result = await _service.UpdateEmployeePhysicalFitnessAsync(model);
            return Ok(result);
        }

        [HttpDelete("DeleteEmployeePhysicalFitness")]
        public async Task<IActionResult> DeleteEmployeePhysicalFitnessAsync(int id)
        {
            var result = await _service.DeleteEmployeePhysicalFitnessAsync(id);
            return Ok(result);
        }

        [HttpGet("GetAllEmployeePhysicalFitnessByEmpId/{empId}")]
        public async Task<IActionResult> GetByEmpId(int empId)
        {
            var result = await _service.GetAllEmployeePhysicalFitnessByEmpId(empId);
            return Ok(result);
        }
    }
}
