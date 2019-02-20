using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.Jwt
{
    public class Permission
    {

        /// <summary>
        /// 关系Id
        /// </summary>
        public string Id { get; set; }


        private string _ApiUrl { get; set; }

        /// <summary>
        /// 后台控制器
        /// </summary>
        public string ApiUrl {
            get {
                return _ApiUrl + ActionName;
            }
            set {
                this._ApiUrl = value;
            }
        }
        /// <summary>
        /// GET POST DEL PUT
        /// </summary>
        public string RequestMethod { get; set; }


        private string _ActionName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName {
            get {
                return _ActionName == "" ? "" : "/" + _ActionName;
            }
            set {
                this._ActionName = value ?? "";
            }
        }

    }
}
