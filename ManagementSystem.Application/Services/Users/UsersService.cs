using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ManagementSystem.Application.Services.Users;

public class UsersService(UserManager<ApplicationUser> _userManager, IHttpContextAccessor _httpContextAccessor) : IUsersService
{
    public async Task<ApplicationUser> GetCurrentUser()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        return user;
    }

    public async Task<List<ApplicationUser>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.ToList();
    }

    public async Task<ApplicationUser> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }
}
