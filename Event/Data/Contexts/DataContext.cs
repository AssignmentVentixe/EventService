using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
}