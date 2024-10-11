using System.ComponentModel.DataAnnotations;

namespace DownloadVideoYouTobe.Modals
{
    public class DownloadVideoModal
    {
        
    }

    public class ReqDownloadYT
    {
        [Required]
        public string LinkYT { get; set; }

        public string? OutputDirectory { get; set; }
        public bool? isHighestVideoQuality { get; set; }
    }
}
