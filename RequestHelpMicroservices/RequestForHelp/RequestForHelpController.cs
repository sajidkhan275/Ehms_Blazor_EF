using Microsoft.AspNetCore.Mvc;

namespace RequestHelpMicroservices.RequestForHelp
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestForHelpController : ControllerBase
    {
        private readonly IRequestForHelpService _service;
        public RequestForHelpController(IRequestForHelpService service)
        {
            _service = service;
        }


        [HttpPost("CreateRequestForHelp")]
        public async Task<IActionResult> CreateRequestForHelpAsync(RequestHelp requestForHelpService)
        {
            int res = await _service.CreateRequestForHelpAsync(requestForHelpService);
            return Ok(res);
        }

        [HttpGet("GetRequestsByEmployeeId/{empId}")]
        public async Task<IActionResult> GetRequestsByEmployeeIdAsync(int empId)
        {
            List<RequestHelp> employees = (await _service.GetRequestsByEmployeeIdAsync(empId)).ToList();
            return Ok(employees);
        }

        [HttpGet("GetRequestsByEmployee")]
        public async Task<IActionResult> GetRequestsByEmployeeAsync()
        {
            List<RequestHelp> employees = (await _service.GetRequestsByEmployeeAsync()).ToList();
            return Ok(employees);
        }

        [HttpPut("UpdateHRRequest")]
        public async Task<IActionResult> UpdateHRRequestAsync(RequestHelp requestForHelpService)
        {
            int res = await _service.UpdateHRRequestAsync(requestForHelpService);
            return Ok(res);
        }

        [HttpDelete("DeleteRequestForHelp")]
        public async Task<IActionResult> DeleteRequestForHelpAsync(int requestForHelpId)
        {
            int res = await _service.DeleteRequestForHelpAsync(requestForHelpId);
            return Ok(res);
        }
    }
}
