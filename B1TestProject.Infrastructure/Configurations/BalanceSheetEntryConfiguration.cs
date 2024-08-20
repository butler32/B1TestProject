using B1TestProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestProject.Infrastructure.Configurations
{
    public class BalanceSheetEntryConfiguration : IEntityTypeConfiguration<BalanceSheetEntryEntity>
    {
        public void Configure(EntityTypeBuilder<BalanceSheetEntryEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.AccountNumber)
                .IsRequired();

            builder
                .HasOne(x => x.ExcelFiles)
                .WithMany(x => x.BalanceSheetEntries)
                .HasForeignKey(x => x.ExcelFilesId);
        }
    }
}
