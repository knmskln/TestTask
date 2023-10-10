using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;

    public OrderService(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Order> GetOrder()
    {
        var order = await _dbContext.Orders
            .OrderByDescending(o => o.Price * o.Quantity)
            .FirstOrDefaultAsync();

        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _dbContext.Orders
            .Where(o => o.Quantity > 10)
            .ToListAsync();

        return orders;
    }
}