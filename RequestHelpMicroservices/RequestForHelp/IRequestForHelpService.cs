namespace RequestHelpMicroservices.RequestForHelp
{
    public interface IRequestForHelpService
    {
        Task<int> CreateRequestForHelpAsync(RequestHelp request);
        Task<IEnumerable<RequestHelp>> GetRequestsByEmployeeIdAsync(int empId);
        Task<int> UpdateHRRequestAsync(RequestHelp request);
        Task<IEnumerable<RequestHelp>> GetRequestsByEmployeeAsync();
        Task<int> DeleteRequestForHelpAsync(int RequestForHelpId);
    }
}
