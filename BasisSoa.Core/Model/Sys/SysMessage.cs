using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 消息表
    /// </summary>
    public class SysMessage : Entity<string>
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// 头像 携带图片
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string Avatar { get; set; }

        /// <summary>
        /// 消息类型 暂定（1通知、待办、3聊天）
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public int MsgType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string Description { get; set; }


        /// <summary>
        /// 着急情况待办用流程 （0未开始、1进行中、2马上到期、3已完成） 
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string MsgStatus { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public bool IsRead { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string CreatorUserId { get; set; }


        /// <summary>
        /// 接收人Id
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string RecriverId { get; set; }


        /// <summary>
        /// 创建人信息
        /// </summary>
        public SysUser sysUser { get; set; } 
    }
}
