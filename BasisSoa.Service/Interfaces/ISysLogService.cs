using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysLogService : IBaseServer<SysLog>
    {
        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="moduleName">方法名称</param>
        /// <param name="nickName">控制器名称</param>
        /// <param name="result">执行结果</param>
        /// <param name="resultData">结果数据</param>
        /// <param name="modelBeta">Token</param>
        void AddLogAsync(string moduleName, string nickName, string result, string resultData, string account, string creatorUserId);
    }
}
