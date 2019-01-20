using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    public class SysLog:Entity<string>
    {
        /// <summary>
        /// Action名称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 操作人账号
        /// </summary>
        public string Account { get; set; }




        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }
}
