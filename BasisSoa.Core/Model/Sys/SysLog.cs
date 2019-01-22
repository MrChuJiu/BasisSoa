using SqlSugar;
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
        [SugarColumn(Length = 64)]
        public string NickName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [SugarColumn(Length = 64)]
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
        [SugarColumn(Length = 64)]
        public string Type { get; set; }
        /// <summary>
        /// 操作人账号
        /// </summary>
        [SugarColumn(Length = 64)]
        public string Account { get; set; }




        /// <summary>
        /// 操作描述
        /// </summary>
        [SugarColumn(Length = 128)]
        public string Description { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [SugarColumn(Length = 64)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }
}
