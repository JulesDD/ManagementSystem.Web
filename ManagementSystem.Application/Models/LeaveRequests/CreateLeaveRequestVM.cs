using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Application.Models.LeaveRequests
{
    public class CreateLeaveRequestVM : IValidatableObject
    {
        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Start Date is required.")]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "End Date is required.")]
        public DateOnly EndDate { get; set; }

        [DisplayName("Select Desired Time Off")]
        [Required(ErrorMessage = "Leave Type is required.")]
        public int LeaveTypeId { get; set; }

        [DisplayName("Additional Information")]
        public string? RequestComments { get; set; }
        public SelectList? LeaveTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(EndDate < StartDate)
            {
                yield return new ValidationResult("End Date cannot be earlier than Start Date.", [ nameof(EndDate), nameof(StartDate)]);
                //new c# shorthand for yield return new ValidationResult("End Date cannot be earlier than Start Date.", [ nameof(EndDate), nameof(StartDate)]);
            }

            // I might change this to a pop-up in the future, but for now I want to encourage employees to provide additional information about their leave request if possible, to help supervisors make informed decisions when reviewing leave requests.
            if (string.IsNullOrWhiteSpace(RequestComments))
            {
                yield return new ValidationResult("Please provide additional information for your leave request, if possible.", [ nameof(RequestComments)]);
            }
        }
    }
}