using DownloadSolution.ViewModels.Account;
using DownloadVideoSolution.ViewModels.Account;
using DownloadVideoSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.System.Accounts
{
    public interface IUsersService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);
        Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request);
        Task<ApiResult<bool>> DeleteUser(Guid id);

    }
}
