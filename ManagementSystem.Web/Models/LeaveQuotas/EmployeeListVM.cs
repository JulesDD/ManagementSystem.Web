using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Web.Models.LeaveQuotas;

public class EmployeeListVM
{
    public string Id { get; set; }
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public string Email { get; set; }
}
