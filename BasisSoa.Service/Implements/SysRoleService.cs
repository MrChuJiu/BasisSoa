using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysRoleService : BaseServer<SysRole>, ISysRoleService
    {
        /// <summary>
        /// 根据角色Id 和 用户Id获取角色详情
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<SysRole> IdAndUserIdQueryModuleAsync(string Id, string UserId)
        {
            var res = Db.Queryable<SysRole>().Mapper(s => s.sysOrganize, s => s.OrganizeId).First();

            return await Task.Run( () => res);
        }
    }
}
