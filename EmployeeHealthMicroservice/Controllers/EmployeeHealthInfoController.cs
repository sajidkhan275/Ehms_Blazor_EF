using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain.Entities;
using EmployeeHealthMicroservice.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHealthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeHealthInfoController : ControllerBase
    {
        private readonly IEmployeeHealthInfoService _service;
        private readonly IWebHostEnvironment _environment;
        public EmployeeHealthInfoController(IEmployeeHealthInfoService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

    
        [HttpPost("CreateEmployeeHealthInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] EmployeeHealthInfo healthInfo, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            if (!file.ContentType.Equals("application/pdf", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file type. Only PDF files are allowed.");
            }
            const long maxFileSize = 10 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                return BadRequest("File size exceeds 10 MB. Please upload a smaller file.");
            }

            string? uploadsPath = Path.Combine(_environment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            byte[] encryptedBytes = FileEncryption.EncryptFile(fileBytes);
            string? filePath = Path.Combine(uploadsPath, Guid.NewGuid() + file.FileName + ".enc");
            await System.IO.File.WriteAllBytesAsync(filePath, encryptedBytes);

            healthInfo.RecentMedicalReportPath = filePath;
            healthInfo.MedicalReportFileName = file.FileName;

            var result = await _service.CreateEmployeeHealthInfoAsync(healthInfo);
            return Ok(result);
        }

        [HttpGet("GetAllEmployeeHealthInfo")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllEmployeeHealthInfo();
            return Ok(result);
        }

        [HttpPut("UpdateEmployeeHealthInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] EmployeeHealthInfo healthInfo, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            if (!file.ContentType.Equals("application/pdf", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file type. Only PDF files are allowed.");
            }
            const long maxFileSize = 10 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                return BadRequest("File size exceeds 10 MB. Please upload a smaller file.");
            }

            string? uploadsPath = Path.Combine(_environment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            byte[] encryptedBytes = FileEncryption.EncryptFile(fileBytes);
            string? filePath = Path.Combine(uploadsPath, Guid.NewGuid() + file.FileName + ".enc");
            await System.IO.File.WriteAllBytesAsync(filePath, encryptedBytes);

            healthInfo.RecentMedicalReportPath = filePath;
            healthInfo.MedicalReportFileName = file.FileName;

            var result = await _service.UpdateEmployeeHealthInfoAsync(healthInfo);
            return Ok(result);
        }

        [HttpDelete("DeleteEmployeeHealthInfo")]
        public async Task<IActionResult> Delete(int id, string medicalReportFileName)
        {
            var result = await _service.DeleteEmployeeHealthInfoAsync(id);
            string? encryptedFilePath = Path.Combine(_environment.ContentRootPath, "uploads", medicalReportFileName);
            if (System.IO.File.Exists(encryptedFilePath))
            {
                System.IO.File.Delete(encryptedFilePath);
            }
            return Ok(result);
        }

        [HttpGet("GetAllEmployeeHealthInfoByEmpId/{empId}")]
        public async Task<IActionResult> GetByEmpId(int empId)
        {
            var result = await _service.GetAllEmployeeHealthInfoByEmpId(empId);
            return Ok(result);
        }

        [HttpGet("DownloadMedicalReport")]
        public async Task<IActionResult> DownloadMedicalReport(string fileName)
        {
            byte[] encryptedBytes = await System.IO.File.ReadAllBytesAsync(fileName);
            byte[] decryptedBytes = FileEncryption.DecryptFile(encryptedBytes);
            string? base64File = Convert.ToBase64String(decryptedBytes);
            string? fileUrl = $"data:application/pdf;base64,{base64File}";
            return Ok(fileUrl);
        }
    }
}
