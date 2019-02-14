using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    public   class SysOrganize:Entity<string>
    {
        /// <summary>
        /// 父级ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string ParentId { get; set; }

        /// <summary>
        /// 类别 集团  公司  部门  小组
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string Category { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary> 
          [SugarColumn(Length = 64,IsNullable = true)]
        public string FullName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string FullNameEn { get; set; }





        /// <summary>
        /// 是否默认展开
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsExpand { get; set; }


        /// <summary>
        /// 删除时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 是否被删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 删除人ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string DeleteUserId { get; set; }



        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 256,IsNullable = true)]
        public string Description { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? SortCode { get; set; }



        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string CreatorUserId { get; set; }



        /// <summary>
        /// 用户
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysUser sysUser { get; set; }





        /// <summary>
        /// 简称
        /// </summary>
        [SugarColumn(Length = 64,IsNullable = true)]
        public string ShortName { get; set; }
        /// <summary>
        /// LOGO
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string Logo { get; set; }
        /// <summary>
        /// 负责人微信
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]

        public string WeChat { get; set; }
        /// <summary>
        /// 工商登记号
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string RegNo { get; set; }
        /// <summary>
        /// 公司位置
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? Layers { get; set; }
        /// <summary>
        /// 支票抬头
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string ChkHead { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>   
           [SugarColumn(Length = 64,IsNullable = true)]
        public string InvHead { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [SugarColumn(Length = 1024, IsNullable = true)]
        public string Address { get; set; }

        /// <summary>
        /// 公司官网
        /// </summary>
        [SugarColumn(Length = 1024, IsNullable = true)]
        public string Website { get; set; }
        /// <summary>
        /// 公司邮箱
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]

        public string Email { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string OrgCode { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string BankNo { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string MobilePhone { get; set; }

      




    }
}
