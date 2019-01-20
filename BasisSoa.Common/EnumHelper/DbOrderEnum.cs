using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.EnumHelper
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum DbOrderEnum
    {
        /// <summary>
        /// 打折
        /// </summary>
        [Text("排序Asc")]
        Asc = 1,

        /// <summary>
        /// 满减
        /// </summary>
        [Text("排序Desc")]
        Desc = 2
    }
}
