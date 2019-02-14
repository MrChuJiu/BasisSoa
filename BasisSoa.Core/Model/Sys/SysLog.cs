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
        /// 操作人账号
        /// </summary>
        [SugarColumn(Length = 64,IsNullable = true)]
        public string Account { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string NickName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 执行结果
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string Result { get; set; }

        /// <summary>
        /// 执行结果
        /// </summary>
        [SugarColumn(Length = 2048, IsNullable = true)]
        public string ResultData { get; set; }

     
  
        /// <summary>
        /// 创建人ID
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreatorTime { get; set; }
    }
}
