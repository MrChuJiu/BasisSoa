using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys.SysUserLogon
{
    public class DetailsSysUserLogonDto: EditSysUserLogonDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LogOnCount { get; set; }
    }
}
