using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Extensions.Jwt;
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
        /// <param name="logType">操作类型</param>
        /// <param name="moduleId">控制器位置</param>
        /// <param name="moduleName">功能</param>
        /// <param name="result">成功失败</param>
        /// <param name="description">描述</param>
        /// <param name="modelBeta">用户授权信息</param>
        /// <returns></returns>
        void AddLogAsync(SysLogEnum logType, string moduleId, string moduleName, bool result, string description, TokenModelBeta modelBeta);
    }
}
