using AutoMapper;
using BasisSoa.Api.ViewModels.Sys;
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
                  .ForMember(s => s.Theme, opts => opts.MapFrom(src => src.sysUserLogon.Theme));
           

            
            //组织配置
            CreateMap<SysOrganize, EditSysOrganizeDto>();
            CreateMap<SysOrganize, DetailsSysOrganizeDto>();
            CreateMap<SysOrganize, TreeListSysOrganizeDto>()
                 .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                .ForMember(s => s.title, ops => ops.MapFrom(src => src.FullName))
                .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId));

            //角色配置
            CreateMap<SysRole, EditSysRoleDto>();
            CreateMap<SysRole, DetailsSysRoleDto>();
            CreateMap<SysRole, TreeListSysRoleDto>()
                  .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                .ForMember(s => s.title, ops => ops.MapFrom(src => src.FullName))
                .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId));
        }
    }
}
