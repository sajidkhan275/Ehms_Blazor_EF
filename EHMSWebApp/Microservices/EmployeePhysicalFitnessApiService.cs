using EHMSModel;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using System.Text;
using System;
using Microsoft.SqlServer.Server;

namespace EHMSWebApp.Microservices
{
    public class EmployeePhysicalFitnessApiService
    {
        private readonly HttpClient _httpClient;
        public EmployeePhysicalFitnessApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEmployeePhysicalFitness(EmployeePhysicalFitness healthInfo)
        {
            var json = JsonSerializer.Serialize(healthInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/EmployeePhysicalFitness/CreateEmployeePhysicalFitness", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEmployeePhysicalFitness(EmployeePhysicalFitness healthInfo)
        {
            var json = JsonSerializer.Serialize(healthInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/EmployeePhysicalFitness/UpdateEmployeePhysicalFitness", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<EmployeePhysicalFitness>> GetEmployeeHealthInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeePhysicalFitness>>("/EmployeePhysicalFitness/GetAllEmployeePhysicalFitness");
                return response ?? new List<EmployeePhysicalFitness>();
            }
            catch (Exception ex) { return new List<EmployeePhysicalFitness>(); }
        }

        public async Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitnessByEmpId(int empId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeePhysicalFitness>>($"/EmployeePhysicalFitness/GetAllEmployeePhysicalFitnessByEmpId/{empId}");
            return response ?? new List<EmployeePhysicalFitness>();
        }

        public async Task DeleteEmployeePhysicalFitness(int id)
        {
            var requestUri = $"/EmployeePhysicalFitness/DeleteEmployeePhysicalFitness?id={id}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }

    }
}
