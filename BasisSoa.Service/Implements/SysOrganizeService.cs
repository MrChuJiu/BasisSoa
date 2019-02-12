using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysOrganizeService:BaseServer<SysOrganize>, ISysOrganizeService
    {
        public async Task<SysOrganize> QuerySysOrganizeByIDAsync(string Id) {
            SysOrganize res = new SysOrganize();

            res = await  Db.Queryable<SysOrganize>()
                .Where(s => s.Id == Id)
                .Mapper(it => it.sysUser, it => it.CreatorUserId)
                .FirstAsync();

            return res;
        }
    }
}
