using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    ///  角色权限 关联 Action表
    /// </summary>
    public class SysRoleAuthorizeAction : Entity<string>
    {
        /// <summary>
        /// 角色权限ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string RoleAuthId {get;set;}
        /// <summary>
        /// ActionID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string ModuleActionId { get; set; }



        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string CreatorUserId { get; set; }



        /// <summary>
        /// 角色授权表信息
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysRoleAuthorize SysRoleAuthorize { get; set; }



        /// <summary>
        /// 模块按钮表信息
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysModuleAction SysModule { get; set; }
    }
}
