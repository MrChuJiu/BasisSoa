using BasisSoa.Common.ClientData;
using BasisSoa.Common.TreeHelper;
using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysRoleAuthorizeService : IBaseServer<SysRoleAuthorize>
    {
        /// <summary>
        /// 获取当前系统所有用户的登录权限和接口访问权限
        /// </summary>
        /// <returns></returns>
       Task<List<RequestApiAuth>> GetRoleModule();
        /// <summary>
        /// 根据角色Id获取权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<CommonTreeModel>> GetRoleModuleByIdAsync(string Id);

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <param name="authorizeActions"></param>
        /// <returns></returns>
        Task<ApiResult<string>> AddSysModuleActionsAsync(string Id, string userId, List<CommonTreeModel> authorizeActions);
    }
}
