using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SysRole:Entity<string>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        [SugarColumn(Length = 64)]
        public string ParentId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>   
        [SugarColumn(Length = 64)]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [SugarColumn(Length = 64)]
        public string Category { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>  
        [SugarColumn(Length = 64)]
        public string FullName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [SugarColumn(Length = 64)]
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
        [SugarColumn(Length = 64)]
        public string DeleteUserId { get; set; }



        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 256)]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool EnabledMark { get; set; }




        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 64)]
        public string CreatorUserId { get; set; }



        /// <summary>
        /// 角色属于哪个组织
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysOrganize sysOrganize { get; set; }
    }
}
