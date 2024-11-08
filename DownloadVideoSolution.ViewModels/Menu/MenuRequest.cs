using DownloadSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.ViewModels.Menu
{
    public class MenuRequest 
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public Guid? ParentId { get; set; }
        public Status Status { get; set; }
    }

    public class MenuStausRequest
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
