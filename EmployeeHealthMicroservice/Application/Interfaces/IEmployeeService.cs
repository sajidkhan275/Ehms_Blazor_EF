using EmployeeHealthMicroservice.Domain;
using EmployeeHealthMicroservice.Domain.Entities;

namespace EmployeeHealthMicroservice.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeDetailsData employee);
        Task<IEnumerable<EmployeeDetailsData>> GetAllEmployeesAsync();
        Task<int> UpdateEmployeeAsync(EmployeeDetailsData employee);
        Task<int> DeleteEmployeeAsync(int empId);
        Task<int> AddRoleAsync(EmployeeRoleData role);
        Task<EmployeeWithRoleDetails> GetRoleEmpWiseAsync(string empId);
        Task<int> DeletRoleEmpWiseAsync(EmployeeRoleData role);
        Task<IEnumerable<EmployeeRoleData>> GetEmpRole();
        Task<IEnumerable<EmployeeRoleData>> GetAllRole();
    }
}
