using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.Data.Entities
{
    public class VideoTranslation
    {
        public int Id { set; get; }
        public int VideoId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Title { set; get; }
        public string LanguageId { set; get; }
        public Language Language { get; set; }
    }
}
