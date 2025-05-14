using Event.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
}