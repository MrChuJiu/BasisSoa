using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysModuleService:BaseServer<SysModule>, ISysModuleService
    {
        /// <summary>
        /// 根据Id获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SysModule> QuerySysModuleByIDAsync(string Id)
        {
            SysModule res = new SysModule();

            res =  Db.Queryable<SysModule>()
                  .Where(s => s.Id == Id)
                  .Mapper(it => it.sysUser, it => it.CreatorUserId)
                  //.Mapper(it => it.sysUser, it => it.CreatorUserId)
                  .First();


            return res;
        }
    }
}
