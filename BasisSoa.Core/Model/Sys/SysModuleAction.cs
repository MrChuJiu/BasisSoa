using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 模块按钮表
    /// </summary>
    public  class SysModuleAction:Entity<string>
    {
        /// <summary>
        /// 模块ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string ModuleId { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string ActionName { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string RequestMethod { get; set; }
        /// <summary>
        /// ACL权限名称（需要拼接）
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string ACL { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 256,IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysUser sysUser { get; set; }


        /// <summary>
        /// 模块表
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysModule SysModule { get; set; }
    } 
}
