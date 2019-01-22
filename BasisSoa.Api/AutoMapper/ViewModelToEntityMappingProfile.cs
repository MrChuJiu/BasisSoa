using AutoMapper;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysUserLogon;
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
        public ViewModelToEntityMappingProfile() {
            //用户配置
            CreateMap<EditSysUserDto, SysUser >();
            CreateMap<DetailsSysUserDto, SysUser>();

            //用户登录信息配置
            CreateMap<EditSysUserLogonDto, SysUserLogon>();
            CreateMap<DetailsSysUserLogonDto, SysUserLogon>();
        }
    }
}
