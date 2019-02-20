using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.Jwt;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysRole;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Common.TreeHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    [Authorize(Policy = "Permission")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SysRoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISysRoleService _sysRoleService;
        private readonly ISysRoleAuthorizeService _sysRoleAuthorizeService;
        public SysRoleController(IMapper mapper, ISysRoleService sysRoleService, ISysRoleAuthorizeService sysRoleAuthorizeService) {
            _mapper = mapper;
            _sysRoleService = sysRoleService;
            _sysRoleAuthorizeService = sysRoleAuthorizeService;
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoleTreeList")]
        public async Task<ApiResult<List<TreeListSysRoleDto>>> GetRoleTreeList(string Id) {
            ApiResult<List<TreeListSysRoleDto>> res = new ApiResult<List<TreeListSysRoleDto>>();
            res.data = new List<TreeListSysRoleDto>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            { 
                var RoleList = _mapper.Map<List<TreeListSysRoleDto>>(await _sysRoleService.QueryAsync(s => s.OrganizeId == Id));

                var sysRole = new List<TreeListSysRoleDto>();
                sysRole.AddRange(RoleList);

                //如果不是管理员
                if (!token.IsAdmin)
                {
                    var sysRoleInfo = sysRole.Where(s => s.key == token.Role).FirstOrDefault();
                    sysRoleInfo.children = new List<TreeListSysRoleDto>();
                    sysRoleInfo.children.AddRange(TreeGenerateTools.TreeGroup(RoleList.Where(s=>s.key != sysRoleInfo.key).ToList(), sysRoleInfo.key));
                    res.data.Add(sysRoleInfo);
                }
                else
                {
                    res.data.AddRange(TreeGenerateTools.TreeGroup(RoleList, "00000000-0000-0000-0000-000000000000"));
                }
             
              

            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }
            return await Task.Run( () => res);
         }

        /// <summary>
        /// 根据Id获取角色详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DetailsSysRoleDto>> Get(string Id) {

            ApiResult<DetailsSysRoleDto> res = new ApiResult<DetailsSysRoleDto>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try {

                res.data = _mapper.Map<DetailsSysRoleDto>( await _sysRoleService.QuerySysModuleByIdAndUserIdAsync(Id, token.Id));
                if (token.Role == Id)
                {
                    res.data.IsDisabled = true;
                }


            } catch (Exception ex) {
                res.code = (int)ApiEnum.Error;
                res.message = "异常："+ex.Message;
            }

            return await Task.Run( () => res);

        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Post(EditSysRoleDto Params) {

            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                string roleId = Guid.NewGuid().ToString();

                SysRole sysRoleInfo = _mapper.Map<SysRole>(Params);
                sysRoleInfo.CreatorTime = DateTime.Now;
                sysRoleInfo.CreatorUserId = token.Id;
                sysRoleInfo.Id = roleId;
                sysRoleInfo.DeleteMark = false;
                var IsSuccess = await _sysRoleService.AddAsync(sysRoleInfo);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：添加角色失败";
                }
                else {
                    //添加权限
                    //Params.treeModels
                    res = await _sysRoleAuthorizeService.AddSysModuleActionsAsync(roleId, token.Id, Params.treeModels);
                }
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<ApiResult<string>> Put(string Id,EditSysRoleDto Params)
        {

            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                SysRole sysRoleInfo = _mapper.Map<SysRole>(Params);
                sysRoleInfo.Id = Id;
                var IsSuccess = await _sysRoleService.UpdateAsync(sysRoleInfo);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：修改角色失败";
                }
                else
                {
                    //添加权限
                    //Params.treeModels
                    res = await _sysRoleAuthorizeService.AddSysModuleActionsAsync(Id, token.Id, Params.treeModels);
                }
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete(string Id) {
            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                var IsSuccess = await _sysRoleService.DeleteAsync(s=>s.Id == Id);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：删除角色失败";
                }
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);

        }

    }
}
