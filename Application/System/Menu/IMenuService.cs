using DownloadSolution.Utilities.Repository;
using DownloadSolution.ViewModels.Menu;
using DownloadVideoSolution.ViewModels.Common;

namespace DownloadSolution.Application.System.Menu
{
    public interface IMenuService : IRepository<DownloadSolution.Data.Entities.Menu>
    {
        Task<ApiResult<bool>> Create(DownloadSolution.Data.Entities.Menu request);
        Task<ApiResult<List<MenuVm>>> GetMenu(string? Name);
        Task<ApiResult<DownloadSolution.Data.Entities.Menu>> Udpate(MenuRequest request);
        Task<ApiResult<bool>> UdpateStatus(MenuStausRequest request);

        /*Task<ApiResult<MenuVm>> GetAll();
        Task<ApiResult<bool>> Create(MenuRequest request);
        Task<ApiResult<bool>> Update(MenuRequest request);
        Task<ApiResult<bool>> Status(MenuStausRequest request);
        Task<ApiResult<bool>> Delete(Guid id);*/

        /*ApiResult<IEnumerable<MenuVm>> GetAll();
        ApiResult<bool> Create(MenuRequest request);
        ApiResult<bool> Update(MenuRequest request);
        ApiResult<bool> Status(MenuStausRequest request);
        ApiResult<bool> Delete(Guid id);*/
    }
}
