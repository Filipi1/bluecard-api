using creditcard_api.Models;
using Microsoft.EntityFrameworkCore;

namespace creditcard_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }

    }
}
