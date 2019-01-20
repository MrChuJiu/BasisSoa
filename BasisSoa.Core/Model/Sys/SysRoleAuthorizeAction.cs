using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    ///  角色权限 关联 Action表
    /// </summary>
    public class SysRoleAuthorizeAction : Entity<Guid>
    {
        /// <summary>
        /// 角色权限ID
        /// </summary>
        public string RoleAuthId {get;set;}
        /// <summary>
        /// ActionID
        /// </summary>
        public string ModuleActionId { get; set; }



        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>

        public string CreatorUserId { get; set; }
    }
}
