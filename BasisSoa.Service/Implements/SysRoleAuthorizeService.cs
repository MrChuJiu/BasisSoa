using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysRoleAuthorizeService : BaseServer<SysRoleAuthorize>, ISysRoleAuthorizeService
    {
        /// <summary>
        /// 获取当前系统所有用户的登录权限和接口访问权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysRoleAuthorize>> GetRoleModule()
        {
            List<SysRoleAuthorize> res = new List<SysRoleAuthorize>();

            res = Db.Queryable<SysRoleAuthorize>()
                .Mapper(it => it.sysRole, it => it.RoleId)
                .Mapper(it => it.sysModule, it => it.ModuleId)
                 .ToList();

            return await Task.Run( () => res);
        }
    }
}
