using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysMessageService : IBaseServer<SysMessage>
    {
        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <returns></returns>
        Task<List<SysMessage>> QuerySysMessageAllAsync();
    }
}
