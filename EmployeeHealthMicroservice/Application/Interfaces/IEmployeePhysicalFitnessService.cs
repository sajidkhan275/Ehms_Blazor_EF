using EmployeeHealthMicroservice.Domain.Entities;

namespace EmployeeHealthMicroservice.Application.Interfaces
{
    public interface IEmployeePhysicalFitnessService
    {
        Task<int> CreateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness fitness);
        Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitness();
        Task<int> UpdateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness fitness);
        Task<int> DeleteEmployeePhysicalFitnessAsync(int employeePhysicalFitnessId);
        Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitnessByEmpId(int empId);
    }
}
