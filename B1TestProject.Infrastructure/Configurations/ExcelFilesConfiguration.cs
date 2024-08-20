using B1TestProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestProject.Infrastructure.Configurations
{
    public class ExcelFilesConfiguration : IEntityTypeConfiguration<ExcelFilesEntity>
    {
        public void Configure(EntityTypeBuilder<ExcelFilesEntity> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasMany(x => x.BalanceSheetEntries)
                .WithOne(x => x.ExcelFiles)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
