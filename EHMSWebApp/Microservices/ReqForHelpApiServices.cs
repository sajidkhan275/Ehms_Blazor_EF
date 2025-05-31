using System.Text.Json;
using System.Text;
using EHMSModel;

namespace EHMSWebApp.Microservices
{
    public class ReqForHelpApiServices
    {
        private readonly HttpClient _httpClient;
        public ReqForHelpApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateRequestForHelp(RequestForHelp data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/RequestForHelp/CreateRequestForHelp", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateHRRequest(RequestForHelp data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/RequestForHelp/UpdateHRRequest", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<RequestForHelp>> GetRequestsByEmployee()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<RequestForHelp>>("/RequestForHelp/GetRequestsByEmployee");
                return response ?? new List<RequestForHelp>();
            }
            catch (Exception ex) { return new List<RequestForHelp>(); }
        }

        public async Task<IEnumerable<RequestForHelp>> GetRequestsByEmployeeId(int empId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<RequestForHelp>>($"/RequestForHelp/GetRequestsByEmployeeId/{empId}");
            return response ?? new List<RequestForHelp>();
        }

        public async Task DeleteRequestForHelp(int id)
        {
            var requestUri = $"/RequestForHelp/DeleteRequestForHelp?requestForHelpId={id}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
