using gRPC.Service.Models;
using Microsoft.EntityFrameworkCore;
namespace gRPC.Service.Data;

public class StoreContext:DbContext
{
    public StoreContext(DbContextOptions<StoreContext> option): base(option)
    {
        
    }
    public DbSet<ToDoItem> ToDoItems { get; set; }
}