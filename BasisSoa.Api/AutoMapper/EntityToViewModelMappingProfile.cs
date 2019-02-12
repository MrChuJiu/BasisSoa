using AutoMapper;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysModule;
using BasisSoa.Api.ViewModels.Sys.SysOrganize;
using BasisSoa.Api.ViewModels.Sys.SysRole;
using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.AutoMapper
{
    /// <summary>
    /// 实体转Dto
    /// </summary>
    public class EntityToViewModelMappingProfile:Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public EntityToViewModelMappingProfile() {
            //用户配置
            CreateMap<SysUser, EditSysUserDto>();
            CreateMap<SysUser, DetailsSysUserDto>()
                  .ForMember(s => s.OrganizeName, opts => opts.MapFrom(src => src.sysOrganize.FullName))
                  .ForMember(s => s.RoleName, opts => opts.MapFrom(src => src.sysRole.FullName))
                  .ForMember(s => s.LogOnCount, opts => opts.MapFrom(src => src.sysUserLogon.LogOnCount))
                  .ForMember(s => s.UserPassword, opts => opts.MapFrom(src => "********"))
                  .ForMember(s => s.LogOnCount, opts => opts.MapFrom(src => src.sysUserLogon.LogOnCount))
                  .ForMember(s => s.MultiUserLogin, opts => opts.MapFrom(src => src.sysUserLogon.MultiUserLogin))
                  .ForMember(s => s.Language, opts => opts.MapFrom(src => src.sysUserLogon.Language))
                  .ForMember(s => s.Theme, opts => opts.MapFrom(src => src.sysUserLogon.Theme))
                  .ForMember(s => s.CreatorUserName, opts => opts.MapFrom(src => src.sysUser.RealName));

            CreateMap<SysUser, AppInitSysUserDto>()
               .ForMember(s => s.sysname, opts => opts.MapFrom(src => src.sysOrganize.FullName))
               .ForMember(s => s.sysdescription, opts => opts.MapFrom(src => src.sysOrganize.Description))
               .ForMember(s => s.name, opts => opts.MapFrom(src => src.RealName))
               .ForMember(s => s.avatar, opts => opts.MapFrom(src => src.HeadIcon))
               .ForMember(s => s.email, opts => opts.MapFrom(src => src.Email));


            //组织配置
            CreateMap<SysOrganize, EditSysOrganizeDto>();
            CreateMap<SysOrganize, DetailsSysOrganizeDto>()
                   .ForMember(s => s.CreatorUserName, ops => ops.MapFrom(src => src.sysUser.RealName));
            CreateMap<SysOrganize, TreeListSysOrganizeDto>()
                 .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                .ForMember(s => s.title, ops => ops.MapFrom(src => src.FullName))
                .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId))
                 .ForMember(s => s.expanded, ops => ops.MapFrom(src => src.IsExpand));
            
            //角色配置
            CreateMap<SysRole, EditSysRoleDto>();
            CreateMap<SysRole, DetailsSysRoleDto>()
                 .ForMember(s => s.CreatorUserName, ops => ops.MapFrom(src => src.sysUser.RealName))
                .ForMember(s=> s.OrganizeName,ops => ops.MapFrom(src => src.sysOrganize.FullName));
            CreateMap<SysRole, TreeListSysRoleDto>()
                  .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                .ForMember(s => s.title, ops => ops.MapFrom(src => src.FullName))
                .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId))
                .ForMember(s => s.expanded, ops => ops.MapFrom(src => src.IsExpand));

            //模块配置
            CreateMap<SysModule, EditSysModuleDto>();
            CreateMap<SysModule, DetailsSysModuleDto>()
                  .ForMember(s => s.CreatorUserName, ops => ops.MapFrom(src => src.sysUser.RealName));
            CreateMap<SysModule, TreeListSysModuleDto>()
                    .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                    .ForMember(s => s.title, ops => ops.MapFrom(src => src.FullName))
                    .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId))
                    .ForMember(s => s.expanded, ops => ops.MapFrom(src => src.IsExpand));

            CreateMap<SysModule, MenuSysModuleDto>()
                    .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                    .ForMember(s => s.text, ops => ops.MapFrom(src => src.FullName))
                    .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId))
                    .ForMember(s => s.icon, ops => ops.MapFrom(src => src.Icon))
                    .ForMember(s => s.i18n, ops => ops.MapFrom(src => ""))
                    .ForMember(s => s.link, ops => ops.MapFrom(src => src.UrlAddress))
                    .ForMember(s => s.reuse, ops => ops.MapFrom(src => true))
                    .ForMember(s => s.hideInBreadcrumb, ops => ops.MapFrom(src => true));

            //模块按钮权限配置
            CreateMap<SysModuleAction, DetailsSysModuleActionDto>()
                   .ForMember(s => s.CreatorUserName, ops => ops.MapFrom(src => src.sysUser.RealName));
            CreateMap<SysModuleAction, EditSysModuleActionDto>();
            
        }
    }
}
