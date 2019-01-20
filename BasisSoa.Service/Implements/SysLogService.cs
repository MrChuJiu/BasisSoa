using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Extensions.Jwt;
using BasisSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Service.Implements
{
    public class SysLogService : BaseServer<SysLog>, ISysLogService
    {
        public async void AddLogAsync(SysLogEnum logType, string moduleId, string moduleName, bool result, string description, TokenModelBeta modelBeta)
        {
            SysLog sysLog = new SysLog();
            sysLog.Id = Guid.NewGuid().ToString();
            sysLog.Date = DateTime.Now;
            sysLog.Account = modelBeta.Name;
            sysLog.Type = logType.GetEnumText();
            sysLog.ModuleId = moduleId;
            sysLog.NickName = moduleName;
            sysLog.Result = result;
            sysLog.Description = description;
            sysLog.CreatorTime = DateTime.Now;
            sysLog.CreatorUserId = modelBeta.Id;
            await Add(sysLog);
                
        }
    }
}
