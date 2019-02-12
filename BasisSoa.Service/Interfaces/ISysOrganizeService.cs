using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysOrganizeService : IBaseServer<SysOrganize>
    {
        /// <summary>
        /// 根据Id获取组织详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<SysOrganize> QuerySysOrganizeByIDAsync(string Id);
    }
}
