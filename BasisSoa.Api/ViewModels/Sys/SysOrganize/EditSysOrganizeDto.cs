using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys
{
    public class EditSysOrganizeDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 类别 集团  公司  部门  小组
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
        /// 是否默认展开
        /// </summary>
        public bool IsExpand { get; set; }




        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool EnabledMark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortCode { get; set; }





        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// LOGO
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 负责人微信
        /// </summary>

        public string WeChat { get; set; }
        /// <summary>
        /// 工商登记号
        /// </summary>
        public string RegNo { get; set; }
        /// <summary>
        /// 公司位置
        /// </summary>
        public int? Layers { get; set; }
        /// <summary>
        /// 支票抬头
        /// </summary>
        public string ChkHead { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>   
        public string InvHead { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 公司官网
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// 公司邮箱
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string MobilePhone { get; set; }
    }
}
