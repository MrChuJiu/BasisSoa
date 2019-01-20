using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 角色模块关系  这块没想后面加扩展所以 就写死了没加类型 
    /// </summary>
    public   class SysRoleAuthorize:Entity<Guid>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModuleId { get; set; }


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
