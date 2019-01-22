using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys.SysUserLogon
{
    public class EditSysUserLogonDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 是否允许多用户登录
        /// </summary>
        public bool MultiUserLogin { get; set; }
        /// <summary>
        /// 系统语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 系统样式
        /// </summary>
        public string Theme { get; set; }

    }
}
