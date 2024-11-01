using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}