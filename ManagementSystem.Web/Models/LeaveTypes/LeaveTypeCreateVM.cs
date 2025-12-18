using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Web.Models.LeaveTypes;

public class LeaveTypeCreateVM
{
    [Required]
    [MinLength(3, ErrorMessage = "Employee name should be more than 3 characters long")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(1, 90, ErrorMessage = "Please enter a valid number of days between 1 and 90")]
    public int NumberOfDays { get; set; }
}
