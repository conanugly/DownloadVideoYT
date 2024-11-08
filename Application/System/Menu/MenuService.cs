using AutoMapper;
using DownloadSolution.Data.EF;
using DownloadSolution.Data.Entities;
using DownloadSolution.Utilities.Repository;
using DownloadSolution.ViewModels.Menu;
using DownloadVideoSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace DownloadSolution.Application.System.Menu
{
    public class MenuService : Repository<DownloadSolution.Data.Entities.Menu>, IMenuService
    {
        private readonly IMapper _mapper;
        public MenuService(SolutionDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ApiResult<bool>> Create(DownloadSolution.Data.Entities.Menu request)
        {
            if(base.Any(x => x.Name.Contains(request.Name)))
                return new ApiErrorResult<bool>("Menu already exists");

            int? num = await base.AddAsync(request);
            if (num > 0)
                return new ApiErrorResult<bool>("Can not create Menu");
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<MenuVm>>> GetMenu(string? Name)
        {
            List<MenuVm> vm = new List<MenuVm>();
            var respon = await base.FindAsync(x => x.Status == Data.Enums.Status.Active);
            if (!string.IsNullOrEmpty(Name))
            {
                respon = respon.Where(x => x.Name.Contains(Name));
            }

            //var data = respon.Join()


            //
            vm = _mapper.Map<List<MenuVm>>(respon.ToList());
            return new ApiSuccessResult<List<MenuVm>>(vm);
        }

        public async Task<ApiResult<DownloadSolution.Data.Entities.Menu>> Udpate(MenuRequest request)
        {
            var res = new DownloadSolution.Data.Entities.Menu();
            var item = await base.FindAsync(x => x.Id == request.Id);
            if (item == null)
                return new ApiErrorResult<DownloadSolution.Data.Entities.Menu>("Can't find Menu.");
            var dataUp = item.First();
            dataUp.Url = request.Url;
            dataUp.Description = request.Description;
            dataUp.Icon = request.Icon;
            dataUp.Name = request.Name;
            dataUp.Status = request.Status;

            res = await base.UpdateAsync(dataUp);
            return new ApiSuccessResult<DownloadSolution.Data.Entities.Menu>(res);
        }

        public Task<ApiResult<bool>> UdpateStatus(MenuStausRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
