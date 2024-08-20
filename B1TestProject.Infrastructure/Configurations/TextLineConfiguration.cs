using B1TestProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestProject.Infrastructure.Configurations
{
    public class TextLineConfiguration : IEntityTypeConfiguration<TextLineEntity>
    {
        public void Configure(EntityTypeBuilder<TextLineEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.Latin)
                .IsRequired();

            builder
                .Property(x => x.Russian)
                .IsRequired();

            builder
                .Property(x => x.IntegerNum)
                .IsRequired();

            builder
                .Property(x => x.DoubleNum)
                .IsRequired();
        }
    }
}
