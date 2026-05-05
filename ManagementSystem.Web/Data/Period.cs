namespace ManagementSystem.Web.Data;

// Period entity to represent the period of leave allocation
public class Period : BaseEntity
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
