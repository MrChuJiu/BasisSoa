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
       Task<List<SysRoleAuthorize>> GetRoleModule();
    }
}
