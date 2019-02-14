using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.Jwt;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysModule;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Common.TreeHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    /// <summary>
    /// 初始化用户信息
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppInitController : Controller
    {
        private readonly ISysUserService _userService;
        private readonly ISysModuleService _sysModuleService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="requirement"></param>
        /// <param name="userService"></param>
        /// <param name="userLogonService"></param>
        /// <param name="mapper"></param>
        public AppInitController(ISysUserService userService, ISysModuleService sysModuleService, IMapper mapper)
        {
            _userService = userService;
            _sysModuleService = sysModuleService;
            _mapper = mapper;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppInitSysUserDto>> Get()
        {
            ApiResult<AppInitSysUserDto> res = new ApiResult<AppInitSysUserDto>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);
            //获取用户信息

            try
            {
                res.data = _mapper.Map<AppInitSysUserDto>(await _userService.QueryAsyncById(token.Id));


                var moduleList = _mapper.Map<List<MenuSysModuleDto>>(await _sysModuleService.QuerySysModuleByRolrIdAsync(token.Role));
                //var oduleList =  _mapper.Map<List<MenuSysModuleDto>>(await _sysModuleService.QueryAsync(s=>s.EnabledMark == true && s.DeleteMark == false))
                res.data.moduleDtos = MenuGenerateTools.MenuGroup(moduleList, "00000000-0000-0000-0000-000000000000");
            }
            catch(Exception ex) {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);
        }


    }
}
