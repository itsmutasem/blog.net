using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> Blogs { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}