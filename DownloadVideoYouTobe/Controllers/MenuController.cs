using AutoMapper;
using DownloadSolution.Application.System.Menu;
using DownloadSolution.Data.Entities;
using DownloadSolution.Utilities;
using DownloadSolution.Utilities.Repository;
using DownloadSolution.ViewModels.Menu;
using DownloadVideoSolution.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;

namespace DownloadSolution.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All menu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            var res = _menuService.GetAll().ToList();
            return Ok(res);
        }

        /// <summary>
        /// get menu status acitve
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get(string? name)
        {
            try
            {
                var res = await _menuService.GetMenu(name);
                return Ok(res);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            //return Ok("thành công");
        }
        /// <summary>
        /// Create new Menu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MenuRequest request)
        {
            Menu data = _mapper.Map<DownloadSolution.Data.Entities.Menu>(request);
            var res = await _menuService.Create(data);
            return Ok(res);
        }

        [HttpPatch]
        public async Task<ActionResult> Update([FromBody] MenuRequest request)
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStatus( [FromBody] MenuStausRequest request)
        {
            return Ok();
        }
    }
}
