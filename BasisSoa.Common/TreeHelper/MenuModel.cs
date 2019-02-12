using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.TreeHelper
{
    /// <summary>
    /// 返回树信息（树公共类）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MenuModel<T>
    {
        public string key { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 前端多语言
        /// </summary>

        public string i18n { get; set; }
        /// <summary>
        /// 是否主导航
        /// </summary>
        public bool group { get; set; }
        /// <summary>
        /// 链接
        /// </summary>

        public string link { get; set; }
        /// <summary>
        /// 图标
        /// </summary>

        public string icon { get; set; }
        /// <summary>
        /// 是否路由复用
        /// </summary>

        public bool reuse { get; set; }
        /// <summary>
        /// Head头默认true
        /// </summary>

        public bool hideInBreadcrumb { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<T> children { get; set; }

    }
}
