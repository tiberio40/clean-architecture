using Core.Entities;
using Core.Entities.Campaign;
using Core.Entities.Scaffolding;
using Core.Entities.Securitty;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<PermissionEntity> PermissionEntity { get; set; }
        public DbSet<RolEntity> RolEntity { get; set; }
        public DbSet<RolesPermissionsEntity> RolesPermissionsEntity { get; set; }
        public DbSet<RolMenusEntity> RolMenusEntity { get; set; }
        public DbSet<MenuEntity> MenuEntity { get; set; }
        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<NormalizeProjectNamesEntity> NormalizeProjectNamesEntity { get; set; }
        public DbSet<ConfigThemeEntity> ConfigThemeEntity { get; set; }
        public DbSet<MetaTypeServiceEntity> MetaTypeServiceEntity { get; set; }
        public DbSet<MarketingStatusEntity> MarketingStatusEntity { get; set; }
        public DbSet<MarketingEntity> MarketingEntity { get; set; }
        public DbSet<MarketingUserEntity> MarketingUserEntity { get; set; }
        public DbSet<MarketingCampaignEntity> MarketingCampaignEntity { get; set; }
        public DbSet<MarketingTemplateEntity> MarketingTemplateEntity { get; set; }
        public DbSet<MetaConfigurationEntity> MetaConfigurationEntity { get; set; }
        public DbSet<RecurringTypeEntity> RecurringTypeEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                      .HasIndex(b => b.Email)
                      .IsUnique();

            modelBuilder.Entity<RolEntity>().Property(t => t.IdRol).ValueGeneratedNever();
            modelBuilder.Entity<PermissionEntity>().Property(t => t.IdPermission).ValueGeneratedNever();

            modelBuilder.Entity<MarketingCampaignEntity>()
                .HasMany(c => c.MarketingUserEntities)
                .WithOne(e => e.MarketingCampaignEntity)
                .HasForeignKey(e => e.MarketingCampaignId);

            modelBuilder.Entity<MarketingCampaignEntity>()
                .HasMany(c => c.MarketingTemplateEntities)
                .WithOne(e => e.MarketingCampaignEntity)
                .HasForeignKey(e => e.MarketingCampaignId);

        }
    }
}
