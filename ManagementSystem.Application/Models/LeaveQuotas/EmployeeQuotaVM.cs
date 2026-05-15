using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Application.Models.LeaveQuotas;

public class EmployeeQuotaVM : EmployeeListVM
{

    [Display(Name = "Date Of Birth")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
    [Display(Name = "Email Address")]

    public List<LeaveQuotaVM> LeaveQuotas { get; set; }

    public bool IsQuotaEmpty { get; set; }
}
