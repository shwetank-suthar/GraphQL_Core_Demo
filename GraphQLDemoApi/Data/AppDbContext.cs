using Microsoft.EntityFrameworkCore;
using GraphQLDemoApi.Models;

namespace GraphQLDemoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<UserLogin> UserLogins => Set<UserLogin>();
    }
}