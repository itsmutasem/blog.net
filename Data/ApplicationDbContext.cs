using Microsoft.EntityFrameworkCore;
using Blog.Models;
using BlogEntity = Blog.Models.Blog;

namespace Blog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<BlogEntity> Blogs => Set<BlogEntity>();
    }
}