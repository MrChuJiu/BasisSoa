using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Api.ApiWebSocket
{
    /// <summary>
    /// 消息基础类
    /// </summary>
   public class NoticetemplateModule
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string datetime { get; set; }
        /// <summary>
        /// 着急状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 接收人Id
        /// </summary>
        public string RecriverId  { get; set; }

}
}
