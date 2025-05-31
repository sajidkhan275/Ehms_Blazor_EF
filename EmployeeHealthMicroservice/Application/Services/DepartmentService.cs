using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain.Entities;
using EmployeeHealthMicroservice.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHealthMicroservice.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeHealthDbContext _dbContext;
        public DepartmentService(EmployeeHealthDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<DepartmentDetails>> GetAllDepartments()
        {
            return await _dbContext.Departments.ToListAsync();
        }
        public async Task<int> CreateDepartments(DepartmentDetails department)
        {
            try
            {
                department.CreatedAt = DateTime.Now;
                await _dbContext.Departments.AddAsync(department);
                await _dbContext.SaveChangesAsync();
                return department.DepartmentId;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<int> DeleteDepartments(int departmentId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //var employeesToUpdate = _dbContext.Employees.Where(e => e.DepartmentId == //departmentId);
                //await employeesToUpdate.ForEachAsync(e => e.DepartmentId = null);

                var departmentToDelete = await _dbContext.Departments.FindAsync(departmentId);
                if (departmentToDelete != null)
                {
                    _dbContext.Departments.Remove(departmentToDelete);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                await LogErrorAsync(ex, "DeleteDepartmentAsync");
                return 0;
            }

        }

        private async Task LogErrorAsync(Exception ex, string procedureName)
        {
            var errorLog = new ErrorLog
            {
                ErrorMessage = ex.Message,
                ErrorSeverity = 16, // Custom severity for EF errors
                ErrorState = 1,     // Custom state
                ErrorProcedure = procedureName,
                ErrorLine = ex.StackTrace != null ? ParseErrorLine(ex.StackTrace) : 0,
                CreatedAt = DateTime.Now
            };
            _dbContext.ErrorLogs.Add(errorLog);
            await _dbContext.SaveChangesAsync();
        }
        private int ParseErrorLine(string stackTrace)
        {
            // Extract the error line number from the stack trace
            // Example: "at Namespace.Class.Method() in File.cs:line 42"
            var lineMatch = System.Text.RegularExpressions.Regex.Match(stackTrace, @"line (\d+)");
            return lineMatch.Success ? int.Parse(lineMatch.Groups[1].Value) : 0;
        }


        public async Task<int> UpdateDepartments(DepartmentDetails department)
        {
            var existingDepartment = await _dbContext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == department.DepartmentId);

            if (existingDepartment == null)
                return 0;

            existingDepartment.DepartmentName = department.DepartmentName;
            existingDepartment.UpdatedAt = DateTime.Now;
            _dbContext.Departments.Update(existingDepartment);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
