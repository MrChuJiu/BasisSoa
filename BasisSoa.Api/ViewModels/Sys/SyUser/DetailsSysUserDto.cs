using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys
{
    /// <summary>
    /// 用户列表数据Dto
    /// </summary>
    public class DetailsSysUserDto:EditSysUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorUserName { get; set; }


        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizeName { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int? LogOnCount { get; set; }
    }
}
