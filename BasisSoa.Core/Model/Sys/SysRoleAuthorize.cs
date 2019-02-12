using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 角色模块关系  这块没想后面加扩展所以 就写死了没加类型 
    /// </summary>
    public   class SysRoleAuthorize:Entity<string>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string RoleId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string ModuleId { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
         [SugarColumn(Length = 64,IsNullable = true)]
        public string CreatorUserId { get; set; }


        /// <summary>
        /// 角色表信息
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysRole sysRole { get; set; }


        /// <summary>
        /// 模块表信息
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysModule sysModule { get; set; }



        /// <summary>
        /// 模块按钮关系表
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<SysRoleAuthorize> sysRoleAuthorizes { get; set; }

    }
}
