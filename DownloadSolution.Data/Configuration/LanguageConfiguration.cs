using DownloadSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DownloadSolution.Data.Configuration
{
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().IsUnicode(false).HasMaxLength(5);
            builder.Property(x => x.IsDefault).IsRequired(true);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(20);
        }
    }
}
