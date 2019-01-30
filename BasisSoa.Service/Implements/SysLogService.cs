using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Service.Implements
{
    public class SysLogService : BaseServer<SysLog>, ISysLogService
    {
        /// <summary>
        /// 添加系统日志
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="nickName"></param>
        /// <param name="result"></param>
        /// <param name="resultData"></param>
        /// <param name="account"></param>
        /// <param name="creatorUserId"></param>
        public async void AddLogAsync(string moduleName, string nickName, string result, string resultData, string account, string creatorUserId)
        {
            SysLog sysLog = new SysLog();
            sysLog.Id = Guid.NewGuid().ToString();
            sysLog.Account = account;
            sysLog.ModuleName = moduleName;
            sysLog.NickName = nickName;
            sysLog.Result = result;
            sysLog.ResultData = resultData;
            sysLog.CreatorTime = DateTime.Now;
            sysLog.CreatorUserId = creatorUserId;
            await AddAsync(sysLog);
                
        }
    }
}
