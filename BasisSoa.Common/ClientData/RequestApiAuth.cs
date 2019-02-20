using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.ClientData
{
    public class RequestApiAuth
    {
        /// <summary>
        /// 关系Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 后台控制器
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// GET POST DEL PUT
        /// </summary>
        public string RequestMethod { get; set; }
        /// <summary>
        /// 具体方法名
        /// </summary>
        public string ActionName { get; set; }
    }
}
