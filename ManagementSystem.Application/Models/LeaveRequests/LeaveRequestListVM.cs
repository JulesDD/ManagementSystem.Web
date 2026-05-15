using System.ComponentModel;
using ManagementSystem.Application.Services.LeaveRequests;

namespace ManagementSystem.Application.Models.LeaveRequests
{
    public class LeaveRequestListVM
    {
        public int Id { get; set; }

        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]
        public DateOnly EndDate { get; set; }

        [DisplayName("Number of Days")]
        public int NumberOfDays { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveType { get; set; }= string.Empty;

        [DisplayName("Request Status")]
        public LeaveRequestStatusEnum LeaveRequestStatus { get; set; }
    }
}