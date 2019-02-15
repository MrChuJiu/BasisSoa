using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.TreeHelper
{
    public class CommonTreeModel:TreeModel<CommonTreeModel>    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// 设置节点禁用 Checkbox
        /// </summary>
        public bool disableCheckbox { get; set; }

        /// <summary>
        /// 0是模块  1是模块按钮
        /// </summary>
        public int type { get; set; }
    }
}
