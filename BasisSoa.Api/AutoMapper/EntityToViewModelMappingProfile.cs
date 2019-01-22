using AutoMapper;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysRole;
using BasisSoa.Api.ViewModels.Sys.SysUserLogon;
using BasisSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.AutoMapper
{
    public class EntityToViewModelMappingProfile:Profile
    {
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
           


            //用户登录信息配置
            CreateMap<SysUserLogon, EditSysUserLogonDto>();
            CreateMap<SysUserLogon, DetailsSysUserLogonDto>();

            //角色信息配置
            CreateMap<SysRole, TreeListSysRoleDto>()
                .ForMember(s => s.key, ops => ops.MapFrom(src => src.Id))
                .ForMember(s => s.title, ops => ops.MapFrom(src => src.FullName))
                .ForMember(s => s.parentId, ops => ops.MapFrom(src => src.ParentId));
        }
    }
}
