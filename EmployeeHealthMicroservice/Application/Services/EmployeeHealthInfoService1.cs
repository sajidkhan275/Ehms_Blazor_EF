using EmployeeHealthMicroservice.Application.Interfaces;
using EmployeeHealthMicroservice.Domain.Entities;
using EmployeeHealthMicroservice.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHealthMicroservice.Application.Services
{
    public class EmployeeHealthInfoService1 : IEmployeeHealthInfoService
    {
        private readonly EmployeeHealthDbContext _context;
        public EmployeeHealthInfoService1(EmployeeHealthDbContext context)
        {
            _context = context;
        }



        public async Task<int> CreateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo)
        {
            _context.EmployeeHealthInfos.Add(healthInfo);
            await _context.SaveChangesAsync();
            return healthInfo.EmployeeHealthInfoId;
        }

        public async Task<int> DeleteEmployeeHealthInfoAsync(int employeeHealthInfoId)
        {
            var entity = await _context.EmployeeHealthInfos.FindAsync(employeeHealthInfoId);
            if (entity != null)
            {
                _context.EmployeeHealthInfos.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<int> UpdateEmployeeHealthInfoAsync(EmployeeHealthInfo healthInfo)
        {
            _context.EmployeeHealthInfos.Update(healthInfo);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfo()
        {
            //var f = _context.Database.SqlQueryRaw<EmployeeHealthInfo>("spname @cat", new SqlParameter("@cat", "abcd")).ToListAsync();

            var empHealth = await (from epf in _context.EmployeeHealthInfos
                                   join emp in _context.Employee
                                   on epf.EmpId equals emp.EmpId
                                   select new EmployeeHealthInfo
                                   {
                                       EmpId = emp.EmpId,
                                       EmployeeHealthInfoId = epf.EmployeeHealthInfoId,
                                       BloodGroup = epf.BloodGroup,
                                       Disability = epf.Disability,
                                       MedicalReportFileName = epf.MedicalReportFileName,
                                       RecentMedicalReportPath = epf.RecentMedicalReportPath,
                                       EmployeeName = emp.EmployeeName

                                   }).ToListAsync();
            return empHealth;

            //left join 
            // await (from epf in _context.EmployeeHealthInfos
            //     join emp in _context.Employee
            //     on epf.EmpId equals emp.EmpId into empGroup
            //     from  emp in empGroup.DefaultIfEmpty()
            //      select new EmployeeHealthInfo
            //     {
            //         EmpId = emp.EmpId,
            //     }).ToListAsync();

            //right join 
            //await (from emp in _context.Employee
            //       join epf in _context.EmployeeHealthInfos
            //    on emp.EmpId  equals epf.EmpId into epfGroup
            //       from epf in epfGroup.DefaultIfEmpty()
            //     select new EmployeeHealthInfo
            //    {
            //        EmpId = emp.EmpId,
            //    }).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeHealthInfo>> GetAllEmployeeHealthInfoByEmpId(int empId)
        {
            var empHealth = await (from epf in _context.EmployeeHealthInfos
                                   join emp in _context.Employee
                                   on epf.EmpId equals emp.EmpId
                                   where emp.EmpId == empId
                                   select new EmployeeHealthInfo
                                   {
                                       EmpId = emp.EmpId,
                                       EmployeeHealthInfoId = epf.EmployeeHealthInfoId,
                                       BloodGroup = epf.BloodGroup,
                                       Disability = epf.Disability,
                                       MedicalReportFileName = epf.MedicalReportFileName,
                                       RecentMedicalReportPath = epf.RecentMedicalReportPath,
                                       EmployeeName = emp.EmployeeName

                                   }).ToListAsync();
            return empHealth;
        }
    }


}
