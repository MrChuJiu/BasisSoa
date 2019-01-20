using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    public class SysUserlogon : Entity<Guid>
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LogOnCount { get; set; }
        /// <summary>
        /// 是否允许多用户登录
        /// </summary>
        public bool MultiUserLogin { get; set; }
        /// <summary>
        /// 用户秘钥
        /// </summary>
        public string UserSecretkey { get; set; }
        /// <summary>
        /// 系统语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 系统样式
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

    }
}
