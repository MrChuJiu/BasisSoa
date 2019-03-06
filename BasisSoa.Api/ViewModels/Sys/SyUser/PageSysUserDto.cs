using BasisSoa.Common.LambdaHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys.SyUser
{
    /// <summary>
    /// 用户列表分页  感觉不对劲 我又不知道咋改
    /// </summary>
    public class PageSysUserDto: DetailsSysUserDto
    {
        [SearchAttribute("Account", Symbol.无效)]
        public int pi { get; set; }
        [SearchAttribute("Account", Symbol.无效)]
        public int ps { get; set; }
    }
}
