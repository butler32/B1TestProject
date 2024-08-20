using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace B1TestProject.Infrastructure
{
    public class TestProjDbContext : DbContext
    {
        public TestProjDbContext(DbContextOptions<TestProjDbContext> options)
            : base(options)
        {
        }

        public DbSet<TextLineEntity> TextLines { get; set; }
        public DbSet<BalanceSheetEntryEntity> BalanceSheetEntries { get; set; }
        public DbSet<ExcelFilesEntity> ExcelFiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TextLineConfiguration());
            modelBuilder.ApplyConfiguration(new BalanceSheetEntryConfiguration());
            modelBuilder.ApplyConfiguration(new ExcelFilesConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
