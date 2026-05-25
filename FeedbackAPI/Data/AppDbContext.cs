using FeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<User> Users { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<Vote> Votes { get; set; }
}