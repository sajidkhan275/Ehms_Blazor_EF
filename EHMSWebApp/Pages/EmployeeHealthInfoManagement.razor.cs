using Azure;
using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace EHMSWebApp.Pages
{
    public partial class EmployeeHealthInfoManagement 
    {
        private List<EmployeeHealthInfo> healthInfo = [];
        private List<EmployeeHealthInfo> filteredhealthInfo = [];
        private List<EmployeeHealthInfo> pagedhealthInfo = [];
        private string searchQuery = string.Empty;
        private bool showAddEditDialog = false;
        private bool isEdit = false;
        private EmployeeHealthInfo currenthealthInfo = new EmployeeHealthInfo();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages;
        private string sortColumn = "EmployeeName";
        private bool ascending = true;
        private IBrowserFile? selectedFile;
        private string StatusMessage = string.Empty;
        private List<int> UserRoles { get; set; } = new List<int>();
        private int EmpId;
        EmployeeWithRoleDetails employeeWithRoleDetails = new();
        private bool healthInfoExist { get; set; }

        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        private IEnumerable<EmployeeHealthInfo> employeeHealthInfoList;
        protected override async Task OnInitializedAsync()
        {
            await LoadDetails();
        }


        private async Task LoadDetails()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            string? entraId = CustomAuthenticationStateProvider.GetEntraId(authState);
            employeeWithRoleDetails = (await employeeService.GetRoleEmpWise(entraId));

            UserRoles = employeeWithRoleDetails.EmployeeRoles!.Select(x => x.RoleId).ToList();
            EmpId = employeeWithRoleDetails.EmployeeDetails!.EmpId;
            if (UserRoles.Any(role => role == (int)UserRole.Admin || role == (int)UserRole.HR))
            {
                healthInfo = (await EmployeeHealthInfoApiService.GetEmployeeHealthInfoAsync()).ToList();             
            }
            else
            {
                healthInfo = (await EmployeeHealthInfoApiService.GetAllEmployeeHealthInfoByEmpId(EmpId)).ToList();
            }
            healthInfoExist = healthInfo.Any(pf => pf.EmpId == EmpId);
            FilterData();
            PaginateData();
        }

        private void FilterData()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredhealthInfo = healthInfo;
            }
            else
            {
                filteredhealthInfo = healthInfo.Where(e => e.EmployeeName!.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.BloodGroup!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.MedicalReportFileName!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private void PaginateData()
        {
            totalPages = (int)Math.Ceiling((double)filteredhealthInfo.Count / pageSize);
            pagedhealthInfo = filteredhealthInfo.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }


        private void OnSearchChange(ChangeEventArgs e)
        {
            searchQuery = e.Value?.ToString() ?? string.Empty;
            currentPage = 1;
            FilterData();
            PaginateData();
        }

        private void SortEmployees(string column)
        {
            if (sortColumn == column)
            {
                ascending = !ascending;
            }
            else
            {
                sortColumn = column;
                ascending = true; 
            }

            switch (sortColumn)
            {
                case "EmployeeName":
                    filteredhealthInfo = ascending ? filteredhealthInfo.OrderBy(e => e.EmployeeName).ToList() : filteredhealthInfo.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                case "BloodGroup":
                    filteredhealthInfo = ascending ? filteredhealthInfo.OrderBy(e => e.BloodGroup).ToList() : filteredhealthInfo.OrderByDescending(e => e.BloodGroup).ToList();
                    break;
                case "MedicalReportFileName":
                    filteredhealthInfo = ascending ? filteredhealthInfo.OrderBy(e => e.MedicalReportFileName).ToList() : filteredhealthInfo.OrderByDescending(e => e.MedicalReportFileName).ToList();
                    break;
                case "Disability":
                    filteredhealthInfo = ascending ? filteredhealthInfo.OrderBy(e => e.Disability).ToList() : filteredhealthInfo.OrderByDescending(e => e.Disability).ToList();
                    break;
                default:
                    filteredhealthInfo = ascending ? filteredhealthInfo.OrderBy(e => e.EmployeeName).ToList() : filteredhealthInfo.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
            }
            PaginateData();
        }

        private string GetSortIcon(string column)
        {
            if (sortColumn != column)
            {
                return "";
            }

            return ascending ? "↑" : "↓";
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                PaginateData();
            }
        }
        private void PrevPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                PaginateData();
            }
        }

        private void OpenAddDialog()
        {
            currenthealthInfo = new EmployeeHealthInfo();
            isEdit = false;
            showAddEditDialog = true;
        }

        private void OpenEditDialog(EmployeeHealthInfo healthInfo)
        {
            currenthealthInfo = healthInfo;
            isEdit = true;
            showAddEditDialog = true;
        }


        private async Task SaveHealthInfo()
        {
            string? uploadsPath = Path.Combine(Environment.WebRootPath, "uploads");
            currenthealthInfo.EmpId = EmpId;
            if (isEdit)
            {
                await EmployeeHealthInfoApiService.UpdateEmployeeHealthInfoAsync(currenthealthInfo, selectedFile);
            }
            else
            {
                await EmployeeHealthInfoApiService.CreateEmployeeHealthInfoAsync(currenthealthInfo, selectedFile);
            }
            showAddEditDialog = false;
            await LoadDetails();
            StatusMessage = "";
        }

        public async Task DownloadMedicalReport(EmployeeHealthInfo healthInfo)
        {
           var response = await EmployeeHealthInfoApiService.DownloadMedicalReport(healthInfo.RecentMedicalReportPath);
            string base64File = await response.Content.ReadAsStringAsync();
            await JSRuntime!.InvokeVoidAsync("downloadFile", base64File, healthInfo.MedicalReportFileName);
        }
        private void CloseDialog()
        {
            showAddEditDialog = false;
        }

        private void HandleFileUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file.ContentType != "application/pdf" || file.Size > 10 * 1024 * 1024)
            {
                StatusMessage = "Invalid file. Only PDF files under 10 MB are allowed.";
                return;
            }
            StatusMessage = "";
            currenthealthInfo.MedicalReportFileName = file.Name;
            selectedFile = file;
        }

        private bool showDeleteDialog = false;
        private int deleteRecordId;

        private void ShowDeleteDialog(int id)
        {
            showDeleteDialog = true;
            deleteRecordId = id;
        }
        private void CloseDeleteDialog()
        {
            showDeleteDialog = false;
            deleteRecordId = 0;
        }

        private async Task HandleDeleteConfirmed(int id)
        {
            string? encryptedFilePath = filteredhealthInfo.Where(x => x.EmployeeHealthInfoId == id).Select(x => x.RecentMedicalReportPath).FirstOrDefault(); 
            await EmployeeHealthInfoApiService.DeleteEmployeeHealthInfoAsync(id, encryptedFilePath);
            showDeleteDialog = false;
            deleteRecordId = 0;
            await LoadDetails();
       
        }
    }
}
