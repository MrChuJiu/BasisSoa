using BasisSoa.Api.ViewModels.Sys.SysModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys
{
    /// <summary>
    /// 初始化用户信息
    /// </summary>
    public class AppInitSysUserDto
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string sysname { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string sysdescription { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public List<MenuSysModuleDto> moduleDtos { get; set; }
    }
}
