using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUser()
    {
        var userWithMostOrders = await _dbContext.Users
            .Include(u => u.Orders)
            .OrderByDescending(u => u.Orders.Count)
            .FirstOrDefaultAsync();

        return userWithMostOrders;
    }

    public async Task<List<User>> GetUsers()
    {
        var inactiveUsers = await _dbContext.Users
            .Where(u => u.Status == UserStatus.Inactive)
            .ToListAsync();

        return inactiveUsers;
    }
}