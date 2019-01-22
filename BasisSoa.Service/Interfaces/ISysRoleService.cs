using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysRoleService : IBaseServer<SysRole>
    {
        /// <summary>
        /// 根据角色 Id 和 用户Id 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<SysRole> IdAndUserIdQueryModuleAsync(string Id,string UserId);
    }
}
