using DownloadSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.Data.Entities
{
    public class VideoDownload
    {
        public long VideoId { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public ICollection<string> Keywords { get; set; }
        public string Engagement { get; set; }
        public Status Status { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Guid DouwnloadById { get; set; }
        public ICollection<Thumbnail> Thumbnail { get; set; }
    }

    public class Thumbnail
    {
        public long Id { get; set; }
        public long VideoId { get; set; }
        public string Url { get; set; }
        public Resolution Resolution { get; set; }
        public VideoDownload VideoDownloads { get; set; }
    }

    public class Resolution
    {
        public long Id { get; set; }
        public int Width { get; set; }

        public int Height { get; set; } 

        public int Area => Width * Height;
        public long ResolutionOfThumbnailId { get; set; }
        public Thumbnail Thumbnails { get; set; }
    }
}
