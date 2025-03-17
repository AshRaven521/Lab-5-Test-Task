using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// UserService implementation.
/// Implement methods here.
/// </summary>
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetUserAsync()
    {
        var user = await _dbContext.Users
            .OrderByDescending(u => u.Sessions.Count)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<List<User>> GetUsersAsync()
    { 
        var mobileSessionUsers = _dbContext.Users
            .Where(u => u.Sessions.Any(s => s.DeviceType == Enums.DeviceType.Mobile));

        if (!mobileSessionUsers.Any())
        {
            return new List<User>();
        }

        return await mobileSessionUsers.ToListAsync();
    }
}
