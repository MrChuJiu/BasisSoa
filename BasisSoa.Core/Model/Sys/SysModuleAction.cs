using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    public  class SysModuleAction:Entity<Guid>
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestMethod { get; set; }
        /// <summary>
        /// 请求描述
        /// </summary>
        public string MethodDescription { get; set; }
        /// <summary>
        /// ACL权限名称（需要拼接）
        /// </summary>
        public string ACL { get; set; }



        /// <summary>
        /// 是否可用
        /// </summary>
        public bool EnabledMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUserId { get; set; }

    } 
}
