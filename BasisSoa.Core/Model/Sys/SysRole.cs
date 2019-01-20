using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SysRole:Entity<Guid>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>           
        public string OrganizeId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>           
        public string FullName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string FullNameEn { get; set; }


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
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool F_EnabledMark { get; set; }

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
