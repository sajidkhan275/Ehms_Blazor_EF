
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestHelpMicroservices.Dcontext;

namespace RequestHelpMicroservices.RequestForHelp
{
    public class RequestForHelpService : IRequestForHelpService
    {
        private readonly EmpReqContext _context;

        public RequestForHelpService(EmpReqContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRequestForHelpAsync(RequestHelp request)
        {
            _context.RequestForHelps.Add(request);
            await _context.SaveChangesAsync();
            return request.RequestForHelpId;
        }

        public async Task<int> DeleteRequestForHelpAsync(int RequestForHelpId)
        {
            var entity = await _context.RequestForHelps.FindAsync(RequestForHelpId);
            if (entity != null)
            {
                _context.RequestForHelps.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<RequestHelp>> GetRequestsByEmployeeAsync()
        {
           var data = await(from req in _context.RequestForHelps
                            join emp in _context.Employee
                            on req.EmpId equals emp.EmpId //into grp
                            //from emp in grp.DefaultIfEmpty()
                            select new RequestHelp{
                              EmpId = req.EmpId,
                              CreatedAt = req.CreatedAt,
                              EmployeeName = emp.EmployeeName,
                              RequestDetails = req.RequestDetails,
                              RequestForHelpId = req.RequestForHelpId,
                              RespondedAt = req.RespondedAt,
                              RespondedStatus = req.RespondedStatus,
                              Status = req.Status   
                            }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<RequestHelp>> GetRequestsByEmployeeIdAsync(int empId)
        {
            var data = await (from req in _context.RequestForHelps
                              join emp in _context.Employee
                              on req.EmpId equals emp.EmpId
                              where emp.EmpId == empId
                              select new RequestHelp
                              {
                                  EmpId = req.EmpId,
                                  CreatedAt = req.CreatedAt,
                                  EmployeeName = emp.EmployeeName,
                                  RequestDetails = req.RequestDetails,
                                  RequestForHelpId = req.RequestForHelpId,
                                  RespondedAt = req.RespondedAt,
                                  RespondedStatus = req.RespondedStatus,
                                  Status = req.Status
                              }).ToListAsync();
            return data;
        }

        public async Task<int> UpdateHRRequestAsync(RequestHelp request)
        {
            _context.RequestForHelps.Update(request);
            return await _context.SaveChangesAsync();
        }
    }
}
