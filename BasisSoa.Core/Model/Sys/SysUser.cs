using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Core.Model.Sys
{
    /// <summary>
    /// 系统用户表
    /// </summary>
     public class SysUser : Entity<string>
    {
        /// <summary>
        /// 登录账号
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string Account { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]

        public string RoleId { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string OrganizeId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(Length = 1024)]
        public string HeadIcon { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string WeChat { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string Tel { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 真名
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string RealName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
          [SugarColumn(Length = 64,IsNullable = true)]
        public string Email { get; set; }

        /// <summary>
        /// 用户Token
        /// </summary>
        [SugarColumn(Length = 1024, IsNullable = true)]
        public string Token { get; set; }

        /// <summary>
        /// 是否管理员（为了功能编辑个人资料中 附带企业信息）
        /// </summary>    
        [SugarColumn(IsNullable = true)]
        public bool? IsAdministrator { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string Description { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? SortCode { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 创建人   迫不得已的写法名字 等作者更新
        /// </summary>
          [SugarColumn(Length = 64, IsNullable = true)]
        public string CreatorId { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysUser sysUser { get; set; }


        /// <summary>
        /// 组织数据
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysOrganize sysOrganize { get; set; }


        /// <summary>
        /// 角色数据
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysRole sysRole { get; set; }


        /// <summary>
        /// 用户登录表数据
        /// </summary>
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public SysUserLogon sysUserLogon { get; set; }

    }

}
