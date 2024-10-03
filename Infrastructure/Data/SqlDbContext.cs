using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<MarketingStatusEntity> MarketingStatusEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

        }
    }
}
