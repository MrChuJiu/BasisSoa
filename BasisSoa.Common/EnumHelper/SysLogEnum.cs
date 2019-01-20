using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.EnumHelper
{
    public enum SysLogEnum
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        [Text("登录日志")]
        Login = 1,

        /// <summary>
        /// 操作日志
        /// </summary>
        [Text("操作日志")]
        Operation = 2,

        /// <summary>
        /// 注册日志
        /// </summary>
        [Text("注册日志")]
        Register = 3,
        /// <summary>
        /// 增加日志
        /// </summary>
        [Text("增加日志")]
        Add = 4,
        /// <summary>
        /// 删除日志
        /// </summary>
        [Text("删除日志")]
        Del = 5,
        /// <summary>
        /// 修改日志
        /// </summary>
        [Text("修改日志")]
        Edit = 6,
        /// <summary>
        /// 查询日志
        /// </summary>
        [Text("查询日志")]
        Select = 7
    }
}
