using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain.Entities;
using EmployeeHealthMicroservice.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHealthMicroservice.Application.Services
{
    public class EmployeePhysicalFitnessService : IEmployeePhysicalFitnessService
    {
        private readonly EmployeeHealthDbContext _context;
        public EmployeePhysicalFitnessService(EmployeeHealthDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness fitness)
        {
            _context.EmpPhysicalFitnessCxt.Add(fitness);
            await _context.SaveChangesAsync();
            return fitness.EmployeePhysicalFitnessId;
        }

        public async Task<int> DeleteEmployeePhysicalFitnessAsync(int employeePhysicalFitnessId)
        {
            var entity = await _context.EmpPhysicalFitnessCxt.FindAsync(employeePhysicalFitnessId);
            if (entity != null)
            {
                _context.EmpPhysicalFitnessCxt.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<int> UpdateEmployeePhysicalFitnessAsync(EmployeePhysicalFitness fitness)
        {
            _context.EmpPhysicalFitnessCxt.Update(fitness);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitness()
        {
            var empPhysics = await (from phy in _context.EmpPhysicalFitnessCxt
                             join emp in _context.Employee
                             on phy.EmpId  equals emp.EmpId 
                                    select new EmployeePhysicalFitness()
                             {
                                 EmpId = phy.EmpId,
                                 EmployeePhysicalFitnessId = phy.EmployeePhysicalFitnessId,
                                 EmployeeName = emp.EmployeeName,
                                 Height = phy.Height,
                                 Weight = phy.Weight
                             }).ToListAsync();
            return empPhysics;
        }

        public async Task<IEnumerable<EmployeePhysicalFitness>> GetAllEmployeePhysicalFitnessByEmpId(int empId)
        {
            var empPhysics = await (from phy in _context.EmpPhysicalFitnessCxt
                                    join emp in _context.Employee
                                    on phy.EmpId equals emp.EmpId
                                    where phy.EmpId == empId
                                    select new EmployeePhysicalFitness()
                                    {
                                        EmpId = phy.EmpId,
                                        EmployeePhysicalFitnessId = phy.EmployeePhysicalFitnessId,
                                        EmployeeName = emp.EmployeeName,
                                        Height = phy.Height,
                                        Weight = phy.Weight
                                    }).ToListAsync();
            return empPhysics;
        }
    }
}
