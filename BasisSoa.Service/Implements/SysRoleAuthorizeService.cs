using BasisSoa.Common.ClientData;
using BasisSoa.Common.TreeHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysRoleAuthorizeService : BaseServer<SysRoleAuthorize>, ISysRoleAuthorizeService
    {
        /// <summary>
        /// 获取当前系统所有用户的登录权限和接口访问权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<RequestApiAuth>> GetRoleModule()
        {
            List<RequestApiAuth> res = new List<RequestApiAuth>();

            res = await Db.Queryable<SysRoleAuthorize, SysRole, SysModule, SysRoleAuthorizeAction, SysModuleAction>
                                    ((sra, sr,sm,sraa,sma) => new object[] {
                                      JoinType.Left,sra.RoleId == sr.Id,
                                      JoinType.Left,sra.ModuleId == sm.Id,
                                      JoinType.Left,sra.Id == sraa.RoleAuthId,
                                      JoinType.Left,sma.Id == sraa.ModuleActionId,
                                    })
                                    .Select((sra, sr, sm, sraa, sma) => new RequestApiAuth
                                    {
                                        Id = sr.Id,
                                        ApiUrl = sm.ApiUrl,
                                        RequestMethod = sma.RequestMethod,
                                        ActionName = sma.ActionName
                                    }).ToListAsync();

            return res;
        }

        /// <summary>
        /// 根据角色Id获取权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<CommonTreeModel>> GetRoleModuleByIdAsync(string Id)
        {
            List<CommonTreeModel> res = new List<CommonTreeModel>();

            res = await Db.UnionAll(Db.Queryable<SysRoleAuthorize, SysModule>((sr, sm) => new object[] {
                                    JoinType.Left,sr.ModuleId == sm.Id})
                                     .Where((sr) => sr.RoleId == Id)
                                     .Select((sr, sm) => new CommonTreeModel
                                     {
                                         key = sm.Id,
                                         parentId = sm.ParentId,
                                         title = sm.FullName,
                                         expanded = sm.IsExpand ?? false,
                                         type = 0,
                                     })
                                    , Db.Queryable<SysRoleAuthorize,SysRoleAuthorizeAction, SysModuleAction>((sr,sra, sma) => new object[] {
                                        JoinType.Left,sr.Id == sra.RoleAuthId,
                                        JoinType.Left,sra.ModuleActionId == sma.Id,
                                   })
                                  .Where((sr) => sr.RoleId == Id)
                                  .Select((sr, sra, sma) => new CommonTreeModel
                                  {
                                      key = sma.Id,
                                      parentId = sma.ModuleId,
                                      title = sma.Description,
                                      expanded = true,
                                      type = 1,
                                  })).ToListAsync();

            return res;

        }
        /// <summary>
        /// 添加 角色对应 模块权限
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="authorizeActions"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddSysModuleActionsAsync(string Id,string userId ,List<CommonTreeModel> authorizeActions)
        {
            ApiResult<string> res = new ApiResult<string>();
            var sysRoleAuth = new List<SysRoleAuthorize>();
            //添加角色授权
            foreach (var item in authorizeActions.Where(s => s.type == 0).ToList()) {
                sysRoleAuth.Add(new SysRoleAuthorize() {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = Id,
                    ModuleId = item.key,
                    CreatorTime = DateTime.Now,
                    CreatorUserId = userId
                });
            }

            #region 删除掉之前的权限

            //得到当前角色现在的权限
            List<string> sysRoleAuthIdList = await Db.Queryable<SysRoleAuthorize>().Where(s => s.RoleId == Id).Select(s => s.Id).ToListAsync();
            //删除掉 角色 模块授权 
            var i = await Db.Deleteable<SysRoleAuthorize>(sysRoleAuthIdList).ExecuteCommandAsync();
            //删除掉 SysRoleAuthorizeAction 角色模块按钮授权
            i = await Db.Deleteable<SysRoleAuthorizeAction>().Where(s => sysRoleAuthIdList.Contains(s.RoleAuthId)).ExecuteCommandAsync();

            #endregion






            //给对应的角色授权加角色模块权限
            i = await Db.Insertable(sysRoleAuth.ToArray()).ExecuteCommandAsync();

            foreach (var item in sysRoleAuth) {
                foreach (var item1 in authorizeActions.Where(s => s.type == 1 && s.parentId == item.ModuleId).ToList())
                {
                    var sysRoleAuthAcions = new List<SysRoleAuthorizeAction>();
                    sysRoleAuthAcions.Add(new SysRoleAuthorizeAction()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleAuthId =  item.Id,
                        ModuleActionId = item1.key,
                        CreatorTime = DateTime.Now,
                        CreatorUserId = userId
                    });
                    int s = await Db.Insertable(sysRoleAuthAcions.ToArray()).ExecuteCommandAsync();
                }
            }

            return res;
        }
    }
}
