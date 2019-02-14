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
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SysRoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISysRoleService _sysRoleService;
        public SysRoleController(IMapper mapper, ISysRoleService sysRoleService) {
            _mapper = mapper;
            _sysRoleService = sysRoleService;
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
                //var RoleList = await _sysRoleService.QueryAsync(s => s.OrganizeId == Id);
                //res.data = TreeGenerateTools.TreeGroup(_mapper.Map<List<TreeListSysRoleDto>>(RoleList), token.Role);

                var RoleList = _mapper.Map<List<TreeListSysRoleDto>>(await _sysRoleService.QueryAsync(s => s.OrganizeId == Id));
                List<TreeListSysRoleDto> TreeList = new List<TreeListSysRoleDto>();
                TreeList.AddRange(RoleList.FindAll(s => s.key == token.Role));
                foreach (var item in TreeList)
                {
                    item.children = new List<TreeListSysRoleDto>();
                    item.children.AddRange(TreeGenerateTools.TreeGroup(RoleList.Where(s => s.key == item.key).ToList(), item.key));
                    res.data.Add(item);
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
                SysRole sysRoleInfo = _mapper.Map<SysRole>(Params);
                sysRoleInfo.CreatorTime = DateTime.Now;
                sysRoleInfo.CreatorUserId = token.Id;
                sysRoleInfo.Id = Guid.NewGuid().ToString();
                sysRoleInfo.DeleteMark = false;
                var IsSuccess = await _sysRoleService.AddAsync(sysRoleInfo);
                if (!IsSuccess) {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：添加角色失败";
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
