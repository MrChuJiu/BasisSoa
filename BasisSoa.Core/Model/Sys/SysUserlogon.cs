using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 用户登录表
    /// </summary>
    public class SysUserLogon : Entity<string>
    {
        /// <summary>
        /// 密码
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string UserPassword { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int? LogOnCount { get; set; }
        /// <summary>
        /// 是否允许多用户登录
        /// </summary>
        public bool? MultiUserLogin { get; set; }
        /// <summary>
        /// 用户秘钥
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string UserSecretkey { get; set; }
        /// <summary>
        /// 系统语言
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string Language { get; set; }
        /// <summary>
        /// 系统样式
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string Theme { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string UserId { get; set; }

    }
}
