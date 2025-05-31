using Azure;
using EHMSModel;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EHMSWebApp.Microservices
{
    public class EmployeeHealthInfoApiService
    {
        private readonly HttpClient _httpClient;
        public EmployeeHealthInfoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo, IBrowserFile file)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(healthInfo.BloodGroup), "BloodGroup");
                formData.Add(new StringContent(healthInfo.EmpId.ToString()), "EmpId");
                formData.Add(new StringContent(healthInfo.Disability.ToString()), "Disability");
                var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                formData.Add(fileContent, "file", file.Name);

                var response = await _httpClient.PostAsync("/EmployeeHealthInfo/CreateEmployeeHealthInfo", formData);
                response.EnsureSuccessStatusCode();
            }
        }
        public async Task UpdateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo, IBrowserFile file)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(healthInfo.BloodGroup), "BloodGroup");
                formData.Add(new StringContent(healthInfo.EmployeeHealthInfoId.ToString()), "EmployeeHealthInfoId");
                formData.Add(new StringContent(healthInfo.EmpId.ToString()), "EmpId");
                formData.Add(new StringContent(healthInfo.Disability.ToString()), "Disability");
                var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                formData.Add(fileContent, "file", file.Name);

                var response = await _httpClient.PutAsync("/EmployeeHealthInfo/UpdateEmployeeHealthInfo", formData);
                response.EnsureSuccessStatusCode();
            }
        }
        public async Task<IEnumerable<EmployeeHealthInfo>> GetEmployeeHealthInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeHealthInfo>>("/EmployeeHealthInfo/GetAllEmployeeHealthInfo");
                return response ?? new List<EmployeeHealthInfo>();
            }
            catch (Exception ex) { return new List<EmployeeHealthInfo>(); }           
        }
        public async Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfoByEmpId(int empId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeHealthInfo>>($"/EmployeeHealthInfo/GetAllEmployeeHealthInfoByEmpId/{empId}");
            return response ?? new List<EmployeeHealthInfo>();
        }

        public async Task DeleteEmployeeHealthInfoAsync(int id, string medicalReportFileName)
        {
            var requestUri = $"/EmployeeHealthInfo/DeleteEmployeeHealthInfo?id={id}&medicalReportFileName={medicalReportFileName}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> DownloadMedicalReport(string fileName)
        {
            var requestUri = $"/EmployeeHealthInfo/DownloadMedicalReport?fileName={fileName}";
            var response = await _httpClient.GetAsync(requestUri);
            return response;

        }
    }
}
