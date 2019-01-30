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
    /// 视图模型到实体模型
    /// </summary>
    public class ViewModelToEntityMappingProfile : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ViewModelToEntityMappingProfile() {
            //用户配置
            CreateMap<EditSysUserDto, SysUser>();
            CreateMap<DetailsSysUserDto, SysUser>();

            //用户登录信息配置
            CreateMap<EditSysUserDto, SysUserLogon>();


            //组织配置
            CreateMap<EditSysOrganizeDto, SysOrganize>();
            CreateMap<DetailsSysOrganizeDto, SysOrganize>();
            CreateMap<TreeListSysOrganizeDto, SysOrganize>();

            //角色配置
            CreateMap<EditSysRoleDto, SysRole>();
            CreateMap<DetailsSysRoleDto, SysRole>();
            CreateMap<TreeListSysRoleDto, SysRole>();

        }
    }
}
