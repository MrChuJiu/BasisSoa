using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysModuleService : IBaseServer<SysModule>
    {
        /// <summary>
        /// 根据Id获取模块详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<SysModule> QuerySysModuleByIDAsync(string Id);
    }
}
