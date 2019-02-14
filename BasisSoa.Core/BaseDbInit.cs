using BasisSoa.Common;
using BasisSoa.Common.EncryptionHelper;
using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Core
{
    public class BaseDbInit
    {
        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="DbContext"></param>
        /// <returns></returns>
        public static async Task SeedAsync(BaseDbContext DbContext)
        {
            try
            {
                // 注意！一定要先手动创建一个空的数据库
                // 会覆盖，可以设置为true，来备份数据
                // 如果生成过了，第二次，就不用再执行一遍了,注释掉该方法即可
                DbContext.CreateTableByEntity(false,typeof(SysLog), 
                                                    typeof(SysUser),
                                                    typeof(SysUserLogon),                               
                                                    typeof(SysOrganize), 
                                                    typeof(SysRole),
                                                    typeof(SysModule),                         
                                                    typeof(SysModuleAction), 
                                                    typeof(SysRoleAuthorize), 
                                                    typeof(SysRoleAuthorizeAction));

                #region 追加种子数据

                string UserId = Guid.NewGuid().ToString();
                string RoleId = Guid.NewGuid().ToString();
                string OrganizeId = Guid.NewGuid().ToString();
                string ModuleId = Guid.NewGuid().ToString();


                string UserSecretkey = Guid.NewGuid().ToString();

                if (!await DbContext.Db.Queryable<SysUser>().AnyAsync())
                {

                    DbContext.Db.Insertable(new SysOrganize() {
                        Id = OrganizeId,
                        ParentId = "00000000-0000-0000-0000-000000000000",
                        Category = "GROUP",
                        FullName = "仓单系统总管理",
                        FullNameEn = "",
                        ShortName = "仓单系统总管理",

                        DeleteMark = false,
                        DeleteUserId = "",
                        DeleteTime = null,

                        Description = "",
                        IsExpand = true,
                        EnabledMark = true,
                        SortCode = 1,
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable(new SysRole()
                    {
                        Id = RoleId,
                        OrganizeId = OrganizeId,
                        ParentId = "00000000-0000-0000-0000-000000000000",
                        Category = "FATHER",
                        FullName = "系统管理员",
                        FullNameEn = "",

                        DeleteMark = false,
                        DeleteUserId = "",
                        DeleteTime = null,

                        Description = "",
                        IsExpand = true,
                        EnabledMark = true,
                        SortCode = 1,
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable(new SysUser()
                    {
                        Id = UserId,
                        RoleId = RoleId,
                        OrganizeId = OrganizeId,
                        Account = "admin",
                        HeadIcon = "https://b-ssl.duitang.com/uploads/item/201508/12/20150812204143_KHCAQ.thumb.700_0.jpeg",
                        WeChat = "",
                        Tel = "",
                        Birthday = null,
                        RealName = "管理员",
                        Email = "guanliyuan@163.com",
                        Token = "",
                        IsAdministrator = true,
                        Description = "",
                        EnabledMark = true,
                        SortCode = 1,
                        CreatorTime = DateTime.Now,
                        CreatorId = "00000000-0000-0000-0000-000000000000"
                    }).ExecuteCommand();
                    DbContext.Db.Insertable(new SysUserLogon()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = UserId,
                        Theme = "",
                        Language = "",
                        UserSecretkey = UserSecretkey,
                        MultiUserLogin = false,
                        LogOnCount = 0,
                        UserPassword = Md5Crypt.Encrypt(DES3Encrypt.EncryptString("123456".ToLower(), UserSecretkey).ToLower(), false).ToLower(),
                        PasswordSecurity = UniversalTool.PassSecurityValidation("123456"),
                    }).ExecuteCommand();
                    DbContext.Db.Insertable(new SysLog()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Account = "系统",
                        NickName = "DbInit",
                        ModuleName = "SeedInitAsync",
                        Result = "OK",
                        ResultData = "初始化数据库所有基础表 和 数据",
                        CreatorUserId = UserId,
                        CreatorTime = DateTime.Now,
                    }).ExecuteCommand();


                  

                    //主菜单
                    DbContext.Db.Insertable<SysModule>(new SysModule() {
                        Id = ModuleId,
                        ParentId = "00000000-0000-0000-0000-000000000000",
                        FullName = "系统",
                        FullNameEn = "",
                        Icon = "",
                        UrlAddress = "",
                        ApiUrl = "",
                        IsMenu = true,
                        IsExpand = true,


                        Description = "系统",
                        SortCode = 99,
                        EnabledMark = true,
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,

                        DeleteMark = false,
                        DeleteTime = null,
                        DeleteUserId = null,
                    }).ExecuteCommand();
                    //子菜单 用户
                    string userModuleId = Guid.NewGuid().ToString();
                    DbContext.Db.Insertable<SysModule>(new SysModule()
                    {
                        Id = userModuleId,
                        ParentId = ModuleId,
                        FullName = "用户管理",
                        FullNameEn = "SysUser",
                        Icon = "",
                        UrlAddress = "/SysAdmin/user",
                        ApiUrl = "/Api/SysUser",
                        IsMenu = false,
                        IsExpand = false,

                        Description = "用户管理",
                        SortCode = 4,
                        EnabledMark = true,

                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,

                        DeleteMark = false,
                        DeleteTime = null,
                        DeleteUserId = null,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction() {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = userModuleId,
                        ActionName = "",
                        RequestMethod = "POST",
                        ACL = "POST",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = userModuleId,
                        ActionName = "",
                        RequestMethod = "PUT",
                        ACL = "PUT",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = userModuleId,
                        ActionName = "",
                        RequestMethod = "GET",
                        ACL = "GET",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = userModuleId,
                        ActionName = "",
                        RequestMethod = "DELETE",
                        ACL = "DELETE",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();


                    //子菜单 角色
                    string roleModuleId = Guid.NewGuid().ToString();
                    DbContext.Db.Insertable<SysModule>(new SysModule()
                    {
                        Id = roleModuleId,
                        ParentId = ModuleId,
                        FullName = "角色管理",
                        FullNameEn = "SysUser",
                        Icon = "",
                        UrlAddress = "/SysAdmin/role",
                        ApiUrl = "/Api/SysRole",
                        IsMenu = false,
                        IsExpand = false,

                        Description = "角色管理",
                        SortCode = 3,
                        EnabledMark = true,

                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,

                        DeleteMark = false,
                        DeleteTime = null,
                        DeleteUserId = null,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = roleModuleId,
                        ActionName = "",
                        RequestMethod = "POST",
                        ACL = "POST",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = roleModuleId,
                        ActionName = "",
                        RequestMethod = "PUT",
                        ACL = "PUT",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = roleModuleId,
                        ActionName = "",
                        RequestMethod = "GET",
                        ACL = "GET",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = roleModuleId,
                        ActionName = "",
                        RequestMethod = "DELETE",
                        ACL = "DELETE",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();


                    //子菜单 组织
                    string organizeModuleId = Guid.NewGuid().ToString();
                    DbContext.Db.Insertable<SysModule>(new SysModule()
                    {
                        Id = organizeModuleId,
                        ParentId = ModuleId,
                        FullName = "组织管理",
                        FullNameEn = "SysUser",
                        Icon = "",
                        UrlAddress = "/SysAdmin/organize",
                        ApiUrl = "/Api/SysOrganize",
                        IsMenu = false,
                        IsExpand = false,

                        Description = "组织管理",
                        SortCode = 2,
                        EnabledMark = true,

                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,

                        DeleteMark = false,
                        DeleteTime = null,
                        DeleteUserId = null,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = organizeModuleId,
                        ActionName = "",
                        RequestMethod = "POST",
                        ACL = "POST",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = organizeModuleId,
                        ActionName = "",
                        RequestMethod = "PUT",
                        ACL = "PUT",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = organizeModuleId,
                        ActionName = "",
                        RequestMethod = "GET",
                        ACL = "GET",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = organizeModuleId,
                        ActionName = "",
                        RequestMethod = "DELETE",
                        ACL = "DELETE",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();

                    //子菜单 模块
                    string moduleModuleId = Guid.NewGuid().ToString();
                    DbContext.Db.Insertable<SysModule>(new SysModule()
                    {
                        Id = moduleModuleId,
                        ParentId = ModuleId,
                        FullName = "模块管理",
                        FullNameEn = "SysUser",
                        Icon = "",
                        UrlAddress = "/SysAdmin/module",
                        ApiUrl = "/Api/SysModule",
                        IsMenu = false,
                        IsExpand = false,

                        Description = "模块管理",
                        SortCode = 1,
                        EnabledMark = true,

                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,

                        DeleteMark = false,
                        DeleteTime = null,
                        DeleteUserId = null,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = moduleModuleId,
                        ActionName = "",
                        RequestMethod = "POST",
                        ACL = "POST",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = moduleModuleId,
                        ActionName = "",
                        RequestMethod = "PUT",
                        ACL = "PUT",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = moduleModuleId,
                        ActionName = "",
                        RequestMethod = "GET",
                        ACL = "GET",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();
                    DbContext.Db.Insertable<SysModuleAction>(new SysModuleAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ModuleId = moduleModuleId,
                        ActionName = "",
                        RequestMethod = "DELETE",
                        ACL = "DELETE",
                        EnabledMark = true,
                        Description = "",
                        CreatorTime = DateTime.Now,
                        CreatorUserId = UserId,
                    }).ExecuteCommand();


                    //角色权限
                    foreach (var item in await DbContext.Db.Queryable<SysModule>().ToListAsync()) {
                        //角色权限
                        DbContext.Db.Insertable<SysRoleAuthorize>(new SysRoleAuthorize()
                        {
                            Id = Guid.NewGuid().ToString(),
                            RoleId = RoleId,
                            ModuleId = item.Id,
                            CreatorTime = DateTime.Now,
                            CreatorUserId = UserId,
                        }).ExecuteCommand();
                    }

                  

                }


                #endregion



            }
            catch (Exception ex)
            {

            }
        }
    }
}
