using EmployeeHealthMicroservice.Domain.Entities;

namespace EmployeeHealthMicroservice.Domain
{
    public class EmployeeWithRoleDetails
    {
        public EmployeeDetailsData? EmployeeDetails { get; set; }
        public List<EmployeeRoleData>? EmployeeRoles { get; set; }
    }
}
