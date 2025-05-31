using EHMSModel;
using System.Text.Json;
using System.Text;

namespace EHMSWebApp.Microservices
{
    public class EmployeeApiService
    {
        private readonly HttpClient _httpClient;
        public EmployeeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEmployee(EmployeeDetails data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Employee/CreateEmployee", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateEmployee(EmployeeDetails data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/Employee/UpdateEmployee", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployees()
        {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDetails>>("/Employee/GetAllEmployees");
                return response ?? new List<EmployeeDetails>();
        }

        public async Task DeleteEmployee(int id)
        {
            var requestUri = $"/Employee/DeleteEmployee?id={id}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddRole(EmployeeRole data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Employee/AddRole", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<EmployeeWithRoleDetails> GetRoleEmpWise(string entraId)
        {
            var requestUri = $"/Employee/GetRoleEmpWise?entraId={entraId}";
            var response = await _httpClient.GetFromJsonAsync<EmployeeWithRoleDetails>(requestUri);
            return response ?? new EmployeeWithRoleDetails();
        }

        public async Task DeletRoleEmpWise(EmployeeRole role)
        {
            var json = JsonSerializer.Serialize(role);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Employee/DeletRoleEmpWise", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<EmployeeRole>> GetEmpRole()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeRole>>("/Employee/GetEmpRole");
            return response ?? new List<EmployeeRole>();
        }

        public async Task<IEnumerable<EmployeeRole>> GetAllRole()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeRole>>("/Employee/GetAllRole");
            return response ?? new List<EmployeeRole>();
        }



    }
}
