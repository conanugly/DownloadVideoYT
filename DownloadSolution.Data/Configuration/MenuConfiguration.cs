using DownloadSolution.Data.Entities;
using DownloadSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DownloadSolution.Data.Configuration
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(15);

            builder.Property(x => x.Status).HasDefaultValue(Status.Active).HasConversion<int>();

            builder.Property(x => x.Description).HasMaxLength(50);

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");
        }
    }
}
