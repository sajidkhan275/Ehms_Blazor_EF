using System.Text.Json;
using System.Text;
using EHMSModel;

namespace EHMSWebApp.Microservices
{
    public class DepartmentApiService
    {
        private readonly HttpClient _httpClient;
        public DepartmentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateDepartments(DepartmentDetails data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Department/CreateDepartments", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDepartments(DepartmentDetails data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/Department/UpdateDepartments", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<DepartmentDetails>> GetAllDepartments()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<DepartmentDetails>>("/Department/GetAllDepartments");
                return response ?? new List<DepartmentDetails>();
            }
            catch (Exception ex) { return new List<DepartmentDetails>(); }
        }

        public async Task DeleteDepartments(int id)
        {
            var requestUri = $"/Department/DeleteDepartments?id={id}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
