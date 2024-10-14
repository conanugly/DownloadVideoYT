using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Video
{
    public interface IVideoService
    {
        Task<bool> IsCheck(string? url, string? id);
        Task<IActionResult> GetDetail();
        Task<IActionResult> Update();
        Task<IActionResult> Delete();
        Task<IActionResult> Insert();
    }
}
