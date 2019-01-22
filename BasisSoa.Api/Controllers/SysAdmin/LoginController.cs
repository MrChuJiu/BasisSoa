using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Extensions.Jwt;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    /// <summary>
    /// 登录注册控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ISysUserService _userService;
        private readonly ISysUserLogonService _userLogonService;
        private readonly IMapper _mapper;
        private readonly PermissionRequirement _requirement;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="requirement"></param>
        /// <param name="userService"></param>
        public LoginController(PermissionRequirement requirement, ISysUserService userService, ISysUserLogonService userLogonService, IMapper mapper)
        {
            _requirement = requirement;
            _userService = userService;
            _userLogonService = userLogonService;
            _mapper = mapper;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<string>> Get(string username, string password)
        {
            ApiResult<string> res = new ApiResult<string>();
            //获取用户信息
            ApiResult<SysUser> apiResult = await _userService.UserNameAndPassQueryAsync(username, password);

            if (apiResult.code != (int)ApiEnum.Status)
            {
                res.code = apiResult.code;
                res.message = apiResult.message;
                return await Task.Run(() => res);
            }

            //修改登录信息 
            var userLogonUp = await _userLogonService.UpdateAsync(c=>new SysUserLogon { LogOnCount = c.LogOnCount +1 }, s => s.UserId == apiResult.data.Id);

            if (userLogonUp)
            {
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Jti, apiResult.data.Id),
                    new Claim(ClaimTypes.Name, apiResult.data.RealName),
                    new Claim(ClaimTypes.Gender, "Web"),
                    new Claim(ClaimTypes.GroupSid, apiResult.data.OrganizeId),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };

                claims.Add(new Claim(ClaimTypes.Role, apiResult.data.RoleId));
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                res.data = JwtToken.BuildJwtToken(claims.ToArray(), _requirement).token;
            }

            return await Task.Run(() => res);
        }


    }
}
