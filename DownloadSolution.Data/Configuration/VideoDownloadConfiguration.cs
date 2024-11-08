using DownloadSolution.Data.Entities;
using DownloadSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DownloadSolution.Data.Configuration
{
    internal class VideoDownloadConfiguration : IEntityTypeConfiguration<VideoDownload>
    {
        public void Configure(EntityTypeBuilder<VideoDownload> builder)
        {
            builder.ToTable("VideoDowloads");
            builder.HasKey(x => x.VideoId);
            
            builder.Property(x => x.Status).HasDefaultValue(Status.Active).HasConversion<int>();
            //builder.Property(x => x.Status).HasDefaultValue(1);

        }
    }

    internal class ThumbnailConfiguration : IEntityTypeConfiguration<Thumbnail>
    {
        public void Configure(EntityTypeBuilder<Thumbnail> builder)
        {
            builder.ToTable("Thumbnails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.VideoId).IsRequired();
            builder.Property(x => x.Url).IsRequired();

            builder.HasOne<VideoDownload>(s => s.VideoDownloads)
                .WithMany(x => x.Thumbnail)
                .HasForeignKey(x => x.VideoId);

            builder.HasOne<Resolution>(s => s.Resolution)
                .WithOne(ad => ad.Thumbnails)
                .HasForeignKey<Resolution>(ad => ad.ResolutionOfThumbnailId);
        }
    }

    internal class ResolutionConfiguration : IEntityTypeConfiguration<Resolution>
    {
        public void Configure(EntityTypeBuilder<Resolution> builder)
        {
            builder.ToTable("Resolutions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
