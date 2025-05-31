using EmployeeHealthMicroservice.Domain.Entities;

namespace EmployeeHealthMicroservice.Application.Interfaces
{
    public interface IEmployeeHealthInfoService
    {
        Task<int> CreateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo);
        Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfo();
        Task<int> UpdateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo);
        Task<int> DeleteEmployeeHealthInfoAsync(int employeeHealthInfoId);
        Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfoByEmpId(int empId);
    }
}
