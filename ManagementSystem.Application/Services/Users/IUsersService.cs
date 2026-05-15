namespace ManagementSystem.Application.Services.Users;

public interface IUsersService
{
    Task<ApplicationUser> GetCurrentUser();
    Task<ApplicationUser> GetUserById(string userId);
    Task<List<ApplicationUser>> GetEmployees();
}