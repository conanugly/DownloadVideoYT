using DownloadVideoSolution.ViewModels.Account;
using DownloadVideoSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.System.Accounts
{
    public interface IAccountService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
    }
}
