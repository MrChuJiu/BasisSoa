using BasisSoa.Common.EnumHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.ClientData
{
    public class ApiResult<T> where T :class
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; } = "";
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = (int)ApiEnum.Status;

        /// <summary>
        /// 数据集
        /// </summary>
        public T data { get; set; }
    }
}
