using Microsoft.EntityFrameworkCore;

using clean_architecture.Domain.Models;

namespace clean_architecture.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}