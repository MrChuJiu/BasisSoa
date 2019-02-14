using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.Jwt;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SyUser;
using BasisSoa.Common;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EncryptionHelper;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SysUserController : Controller
    {
        private readonly ISysUserService _userService;
        private readonly ISysLogService _logService;
        private readonly ISysUserLogonService _userLogonService;
        private readonly IMapper _mapper;

        public SysUserController(ISysUserService userService, ISysUserLogonService userLogonService, ISysLogService logService, IMapper mapper)
        {
            _userService = userService;
            _logService = logService;
            _userLogonService = userLogonService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取用户列表  PageSysUserDto
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AngularSTResult<DetailsSysUserDto>> Get(int pi,int ps) {
           
          
            AngularSTResult<DetailsSysUserDto> res = new AngularSTResult<DetailsSysUserDto>();
            //SearchClass<SysUser,EditSysUserDto>.GetWhereLambda(Params)
            var userList = await _userService.UserQueryAsync(null,pi,ps,s=>s.CreatorTime,true);
            res.list = _mapper.Map<List<DetailsSysUserDto>>(userList);
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获取用户详情 个人中心信息编辑
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserDetails")]
        public async Task<ApiResult<DetailsSysUserDto>> GetUserDetails(string Id)
        {
            ApiResult<DetailsSysUserDto> res = new ApiResult<DetailsSysUserDto>();
            res.data = _mapper.Map<DetailsSysUserDto>(await _userService.QueryAsyncById(Id));
            return await Task.Run(() => res);
        }


        /// <summary>
        /// 获取用户详情根据Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTokenUserDetails")]
        public async Task<ApiResult<DetailsSysUserDto>> GetTokenUserDetails()
        {
            ApiResult<DetailsSysUserDto> res = new ApiResult<DetailsSysUserDto>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);
            res.data = _mapper.Map<DetailsSysUserDto>(await _userService.QueryAsyncById(token.Id));
            return await Task.Run(() => res);
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Post(EditSysUserDto Params) {

            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);
            
            //开启事务
            try { 
                SysUser userInfo = _mapper.Map<SysUser>(Params);
                userInfo.Id = Guid.NewGuid().ToString();
                userInfo.CreatorTime = DateTime.Now;
                userInfo.CreatorId = token.Id;
                await _userService.AddAsync(userInfo);

                SysUserLogon userLogonInfo = _mapper.Map<SysUserLogon>(Params);
                userLogonInfo.Id = Guid.NewGuid().ToString();
                userLogonInfo.UserId = userInfo.Id;
                userLogonInfo.LogOnCount = 0;
                userLogonInfo.UserSecretkey = Md5Crypt.Encrypt(Guid.NewGuid().ToString());
                userLogonInfo.UserPassword = Md5Crypt.Encrypt(DES3Encrypt.EncryptString(userLogonInfo.UserPassword.ToLower(), userLogonInfo.UserSecretkey).ToLower(), false).ToLower();
                userLogonInfo.PasswordSecurity = UniversalTool.PassSecurityValidation(userLogonInfo.UserPassword);
                await _userLogonService.AddAsync(userLogonInfo);

            }
            catch(Exception ex){
                res.code = (int)ApiEnum.Error;
                res.message = "异常："+ex.Message;
            }

            //事务结束


            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<ApiResult<string>> Put(string Id,EditSysUserDto Params) {
            ApiResult<string> res = new ApiResult<string>();


            //开启事务
            try
            {

                SysUser userInfo = _mapper.Map<SysUser>(Params);
                if (!UniversalTool.ModuleIsNull(userInfo)) {
                    userInfo.Id = Id;
                    await _userService.UpdateAsync(userInfo);
                }


                if (string.IsNullOrEmpty(Params.UserPassword) || Params.UserPassword == "********")
                {
                    Params.UserPassword = null;
                    Params.PasswordSecurity = null;
                }
                SysUserLogon userLogonInfo = _mapper.Map<SysUserLogon>(Params);
                if (!UniversalTool.ModuleIsNull(userLogonInfo))
                {

                   SysUserLogon sysUserLogon = await _userLogonService.QueryFirstAsync(s => s.UserId == Id);

                    if (!string.IsNullOrEmpty(Params.UserPassword) && Params.UserPassword != "********")
                    {
                        userLogonInfo.PasswordSecurity = UniversalTool.PassSecurityValidation(userLogonInfo.UserPassword);
                        userLogonInfo.UserSecretkey = Md5Crypt.Encrypt(Guid.NewGuid().ToString());
                        userLogonInfo.UserPassword = Md5Crypt.Encrypt(DES3Encrypt.EncryptString(userLogonInfo.UserPassword.ToLower(), userLogonInfo.UserSecretkey).ToLower(), false).ToLower();
                    }
                    userLogonInfo.Id = sysUserLogon.Id;
                    await _userLogonService.UpdateAsync(userLogonInfo);
                }

               

            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            //事务结束

            return await Task.Run( () => res);
             

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete(string [] Ids) {

            ApiResult<string> res = new ApiResult<string>();

            try
            {
                //假删除
               var IsSucess =  await  _userService.DeleteAsync(s => Ids.Contains(s.Id));
               if (!IsSucess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：删除失败";
                }

            }
            catch (Exception ex) {

                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run( () => res);
        }

    }
}
