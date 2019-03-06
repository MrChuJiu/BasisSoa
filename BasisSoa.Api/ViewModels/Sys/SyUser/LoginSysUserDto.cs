using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys.SyUser
{
    /// <summary>
    /// 登录成功后的认证信息
    /// </summary>
    public class LoginSysUserDto
    {
        public string token { get; set; }
        public string expires { get; set; }

    }
}
