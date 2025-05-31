using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain;
using EmployeeHealthMicroservice.Domain.Entities;
using EmployeeHealthMicroservice.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHealthMicroservice.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeHealthDbContext _context;

        public EmployeeService(EmployeeHealthDbContext dbContext)
        {
            _context = dbContext;
        }


        public async Task<int> CreateEmployeeAsync(EmployeeDetailsData employee)
        {
            if (await _context.Employee.AnyAsync(e => e.AzureEntraId == employee.AzureEntraId))
                return 0;

            var employeeDetail = new EmployeeDetailsData
            {
                EmployeeCode = employee.EmployeeCode,
                EmployeeName = employee.EmployeeName,
                DepartmentId = employee.DepartmentId,
                JobTitle = employee.JobTitle,
                Email = employee.Email,
                AzureEntraId = employee.AzureEntraId,
                EmployeeRoles = new List<EmployeeRoleData>
                {
                    new EmployeeRoleData
                    {
                        RoleId = employee.RoleId
                    }
                }
            };

            _context.Employee.Add(employeeDetail);
            await _context.SaveChangesAsync();


            //var employeeRole = new EmployeeRoleData
            //{
            //    EmpId = employeeDetail.EmpId,
            //    RoleId = employee.RoleId
            //};
            //
            //_context.EmployeeRoles.Add(employeeRole);
            //await _context.SaveChangesAsync();
            return employee.EmpId;
        }

        public async Task<int> UpdateEmployeeAsync(EmployeeDetailsData employee)
        {
            var existingEmployee = await _context.Employee.FirstOrDefaultAsync(e => e.EmpId == employee.EmpId);

            if (existingEmployee == null)
                return 0;

            existingEmployee.EmployeeCode = employee.EmployeeCode;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.JobTitle = employee.JobTitle;
            _context.Employee.Update(existingEmployee);
            await _context.SaveChangesAsync();
            return existingEmployee.EmpId;
        }

        public async Task<IEnumerable<EmployeeDetailsData>> GetAllEmployeesAsync()
        {
            try
            {
                var empWithDept = await (from e in _context.Employee
                                         join d in _context.Departments
                                          on e.DepartmentId equals d.DepartmentId into deptGroup
                                         from dept in deptGroup.DefaultIfEmpty()
                                         select new EmployeeDetailsData
                                         {
                                             EmpId = e.EmpId,
                                             EmployeeCode = e.EmployeeCode,
                                             EmployeeName = e.EmployeeName,
                                             DepartmentId = e.DepartmentId,
                                             JobTitle = e.JobTitle,
                                             Email = e.Email,
                                             AzureEntraId = e.AzureEntraId,
                                             DepartmentName = dept != null ? dept.DepartmentName : null
                                         }).ToListAsync();
                return empWithDept;
            }
            catch (Exception)
            {
                return new List<EmployeeDetailsData>();
            }
        }

        public async Task<int> DeleteEmployeeAsync(int empId)
        {
            var employee = await _context.Employee.Include(e => e.EmployeeRoles).FirstOrDefaultAsync(e => e.EmpId == empId);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> AddRoleAsync(EmployeeRoleData role)
        {
            try
            {
                var employeeRole = new EmployeeRoleData
                {
                    EmpId = role.EmpId,
                    RoleId = role.RoleId
                };
                _context.EmployeeRoles.Add(employeeRole);
                await _context.SaveChangesAsync();
                return role.RoleId;
            }
            catch (Exception ex) { return 0; }

        }

        public async Task<EmployeeWithRoleDetails> GetRoleEmpWiseAsync(string empId)
        {
            try
            {
                var result = await _context.Employee
                   .Where(e => e.AzureEntraId == empId)
                   .Select(e => new EmployeeWithRoleDetails
                   {
                       EmployeeDetails = new EmployeeDetailsData
                       {
                           EmpId = e.EmpId,
                           EmployeeCode = e.EmployeeCode ?? string.Empty,
                           EmployeeName = e.EmployeeName,
                           Email = e.Email,
                           AzureEntraId = e.AzureEntraId,
                           DepartmentId = e.DepartmentId ?? 0,
                           JobTitle = e.JobTitle ?? string.Empty
                       },
                       EmployeeRoles = (from er in _context.EmployeeRoles
                                        where er.EmpId == e.EmpId
                                        join r in _context.Roles
                                        on er.RoleId equals r.RoleId
                                        select new EmployeeRoleData 
                                        {
                                            EmpId = er.EmpId,
                                            RoleId = er.RoleId,
                                            EmpRoleId = er.EmpRoleId,

                                        }).ToList()
                       //EmployeeRoles = _context.EmployeeRoles
                       //    .Where(er => er.EmpId == e.EmpId)
                       //    .Join(_context.Roles,
                       //          er => er.RoleId,
                       //          r => r.RoleId,
                       //          (er, r) => new EmployeeRole
                       //          {
                       //              EmpRoleId = er.EmpRoleId,
                       //              EmpId = er.EmpId,
                       //              RoleId = er.RoleId
                       //          })
                       //    .ToList()
                   })
                   .FirstOrDefaultAsync();

                return result ?? new EmployeeWithRoleDetails();

            }
            catch (Exception ex)
            {
                return new EmployeeWithRoleDetails();
            }
        }

        public async Task<int> DeletRoleEmpWiseAsync(EmployeeRoleData role)
        {
            var employee = await _context.EmployeeRoles.Where(e => e.EmpId == role.EmpId).ToListAsync();
            if (employee != null)
            {
                _context.EmployeeRoles.RemoveRange(employee);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<IEnumerable<EmployeeRoleData>> GetEmpRole()
        {
            var employeeRoleSummary = await _context.Employee.Select(e => new EmployeeRoleData
            {
                EmpId = e.EmpId,
                EmployeeName = e.EmployeeName,
                Name = string.Join(", ", _context.EmployeeRoles
                .Where(er => er.EmpId == e.EmpId)
                .Join(_context.Roles, er => er.RoleId, r => r.RoleId, (er, r) => r.Name)
                .ToList()),
                RolesId = string.Join(", ", _context.EmployeeRoles
                .Where(er => er.EmpId == e.EmpId)
                .Select(er => er.RoleId.ToString())
                .ToList())
            }).ToListAsync();
            return employeeRoleSummary;
        }

        public async Task<IEnumerable<EmployeeRoleData>> GetAllRole()
        {
            var a = await _context.Roles.Select(e => new EmployeeRoleData
            {
                RoleId = e.RoleId,
                Name = e.Name,
            }).ToListAsync();
            return a;
        }
    }
}
