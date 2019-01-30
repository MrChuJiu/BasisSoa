using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys
{
    public class EditSysModuleDto
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
        /// 英文名称
        /// </summary>
        public string FullNameEn { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

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
        public bool IsMenu { get; set; }
        /// <summary>
        /// 是否默认展开
        /// </summary>
        public bool IsExpand { get; set; }


 

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
        /// 模块权限列表
        /// </summary>
        public List<DetailsSysModuleActionDto> SysModuleActionDtos { get; set; }

    }
}
