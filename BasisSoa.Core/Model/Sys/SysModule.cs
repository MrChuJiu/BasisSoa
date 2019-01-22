using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 系统模块表
    /// </summary>
    public class SysModule:Entity<Guid>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 前台路由
        /// </summary>
        public string UrlAddress { get; set; }
        /// <summary>
        /// 后台接口(控制器名字)
        /// </summary>
        public string ApiUrl { get; set; }


        /// <summary>
        /// 是不是菜单
        /// </summary>
        public bool? IsMenu { get; set; }
        /// <summary>
        /// 是否默认展开
        /// </summary>
        public bool? IsExpand { get; set; }


        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool DeleteMark { get; set; }
        /// <summary>
        /// 删除人ID
        /// </summary>
        public string DeleteUserId { get; set; }





        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? EnabledMark { get; set; }



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
