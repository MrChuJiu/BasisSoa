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
        public string id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 消息类型 暂定（1通知、待办、3聊天）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 着急情况待办用流程 （0未开始、1进行中、2马上到期、3已完成） 
        /// </summary>
        public string extra { get; set; }
        /// <summary>
        /// 就是个颜色
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool read { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string datetime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creatorUserName { get; set; }

        /// <summary>
        /// 接收人Id
        /// </summary>
        public string RecriverId { get; set; }

}
}
