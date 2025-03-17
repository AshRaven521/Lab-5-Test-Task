using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        var earliestSession = await _dbContext.Sessions
            .Where(s => s.DeviceType == Enums.DeviceType.Desktop)
            .OrderBy(d => d.StartedAtUTC)
            .FirstOrDefaultAsync();

        return earliestSession;
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        var sessions = _dbContext.Sessions
            .Where(s => s.User.Status == Enums.UserStatus.Active)
            .Where(s => s.EndedAtUTC.Year < 2025);

        if (!sessions.Any())
        {
            return new List<Session>();
        }

        return await sessions.ToListAsync();
    }
}
