using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.Jwt;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysModule;
using BasisSoa.Common;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Common.TreeHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasisSoa.Api.Controllers.SysAdmin
{
    [Authorize(Policy = "Permission")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SysModuleController : Controller
    {
        private readonly ISysRoleService _sysRoleService;
        private readonly ISysModuleService _sysModuleService;
        private readonly ISysModuleActionService _sysModuleActionService;
        private readonly ISysRoleAuthorizeService _sysRoleAuthorizeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sysModuleService"></param>
        /// <param name="mapper"></param>
        public SysModuleController(ISysModuleService sysModuleService, ISysModuleActionService sysModuleActionService, ISysRoleAuthorizeService sysRoleAuthorizeService, ISysRoleService sysRoleService, IMapper mapper) {
            _sysModuleService = sysModuleService;
            _sysModuleActionService = sysModuleActionService;
            _sysRoleAuthorizeService = sysRoleAuthorizeService;
            _sysRoleService = sysRoleService;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取模块树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetModuleTreeList")]
        public async Task<ApiResult<List<TreeListSysModuleDto>>> GetModuleTreeList() {

            ApiResult<List<TreeListSysModuleDto>> res = new ApiResult<List<TreeListSysModuleDto>>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);


            try {
                var ModuleList = await  _sysModuleService.QueryAsync(s=>s.DeleteMark == false && s.EnabledMark == true);
                res.data = TreeGenerateTools.TreeGroup(_mapper.Map<List<TreeListSysModuleDto>>(ModuleList), "00000000-0000-0000-0000-000000000000");
            }
            catch (Exception ex) {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run( () => res);
        }

        /// <summary>
        /// 获取权限模块树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetModuleTreeAuthList")]
        public async Task<ApiResult<List<CommonTreeModel>>> GetModuleTreeAuthList(string Id)
        {

            ApiResult<List<CommonTreeModel>> res = new ApiResult<List<CommonTreeModel>>();
            res.data = new List<CommonTreeModel>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                //获取可以看到的 模块
                var ModuleList = await _sysRoleAuthorizeService.GetRoleModuleByIdAsync(token.Role);
                if (ModuleList.Count > 0)
                {
                    //查询这个角色是否是菜单
                    var role = await _sysRoleService.QueryByIDAsync(Id);
                    //如果不是菜单
                    if (!string.IsNullOrEmpty(Id) && !UniversalTool.ModuleIsNull(role))
                    {
                        //获取传入角色的权限信息
                        List<string> roleKeyList = (await _sysRoleAuthorizeService.GetRoleModuleByIdAsync(Id)).Select(s=>s.key).ToList() ;
                        ModuleList.Where(a => roleKeyList.Contains(a.key) && a.type == 1).ToList().ForEach(a => a.@checked = true);
                        //如果打开的是自己的就禁用
                        if (Id == token.Role) {
                            ModuleList.ForEach(a => a.disableCheckbox = true);
                        }
                   
                    }

                    res.data.AddRange(TreeGenerateTools.TreeGroup(ModuleList, "00000000-0000-0000-0000-000000000000"));
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
        /// 获取模块详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DetailsSysModuleDto>> Get(string Id) {

            ApiResult<DetailsSysModuleDto> res = new ApiResult<DetailsSysModuleDto>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                res.data = _mapper.Map<DetailsSysModuleDto>(await _sysModuleService.QuerySysModuleByIDAsync(Id));
                res.data.SysModuleActionDtos = _mapper.Map<List<DetailsSysModuleActionDto>>(await _sysModuleActionService.QueryAsync(s => s.ModuleId == Id));
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);
        }

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Post(EditSysModuleDto Params) {
            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);
            try {
                //添加模块
                SysModule sysModuleInfo = _mapper.Map<SysModule>(Params);
                sysModuleInfo.CreatorTime = DateTime.Now;
                sysModuleInfo.CreatorUserId = token.Id;
                sysModuleInfo.Id = Guid.NewGuid().ToString();
                sysModuleInfo.DeleteMark = false;
                var IsSuccess = await _sysModuleService.AddAsync(sysModuleInfo);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：添加模块失败";
                }

                //添加模块请求
                List<SysModuleAction> AddsysModuleActionList = _mapper.Map<List<SysModuleAction>>(Params.SysModuleActionDtos.Where(s => s.Id == null));
                if (AddsysModuleActionList.Count() > 0) {
                    foreach (var item in AddsysModuleActionList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.ModuleId = sysModuleInfo.Id;
                        item.CreatorTime = DateTime.Now;
                        item.CreatorUserId = token.Id;
                    }
                    IsSuccess = await _sysModuleActionService.AddListAsync(AddsysModuleActionList);
                    if (!IsSuccess)
                    {
                        res.code = (int)ApiEnum.Failure;
                        res.message = "错误：添加模块失败";
                    }

                }
              

            } catch (Exception ex) {
                res.code = (int)ApiEnum.Error;
                res.message = "异常："+ex.Message;
            }

            return await Task.Run( () => res);
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        [HttpPut("{Id}")]
        public async Task<ApiResult<string>> Put(string Id,EditSysModuleDto Params) {
            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);
            try
            {
                //添加模块
                SysModule sysModuleInfo = _mapper.Map<SysModule>(Params);
                sysModuleInfo.Id = Id;
                var IsSuccess = await _sysModuleService.UpdateAsync(sysModuleInfo);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：修改模块失败";
                }

                List<string> ModuleIds = new List<string>();

                //修改模块数据
                List<SysModuleAction> EditsysModuleActionList = _mapper.Map<List<SysModuleAction>>(Params.SysModuleActionDtos.Where(s => s.Id != null));
                if (EditsysModuleActionList.Count() > 0)
                {
                    IsSuccess = await _sysModuleActionService.UpdateListAsync(EditsysModuleActionList);
                    if (!IsSuccess)
                    {
                        res.code = (int)ApiEnum.Failure;
                        res.message = "错误：修改模块失败";
                    }
                    ModuleIds.AddRange(EditsysModuleActionList.Select(s => s.Id).ToList());
                }
             

                //添加模块请求
                List<SysModuleAction> AddsysModuleActionList = _mapper.Map<List<SysModuleAction>>(Params.SysModuleActionDtos.Where(s=>s.Id == null));
                if (AddsysModuleActionList.Count() > 0)
                {
                    foreach (var item in AddsysModuleActionList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.ModuleId = sysModuleInfo.Id;
                        item.CreatorTime = DateTime.Now;
                        item.CreatorUserId = token.Id;
                    }
                    IsSuccess = await _sysModuleActionService.AddListAsync(AddsysModuleActionList);
                    if (!IsSuccess)
                    {
                        res.code = (int)ApiEnum.Failure;
                        res.message = "错误：添加模块失败";
                    }
                    ModuleIds.AddRange(AddsysModuleActionList.Select(s => s.Id).ToList());
                }

                //删除不存在的模块请求
                await _sysModuleActionService.DeleteAsync(s => s.ModuleId == sysModuleInfo.Id && !ModuleIds.Contains(s.Id));
            }
            catch (Exception ex) {
                res.code = (int)ApiEnum.Error;
                res.message = "异常："+ex.Message;
            }

            return await Task.Run( () => res);

        }

        /// <summary>
        /// 删除模块
        /// </summary>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete(string Id) {
            ApiResult<string> res = new ApiResult<string>();

            try
            {
                var IsSuccess = await _sysModuleService.DeleteAsync(s => s.Id == Id);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：删除模块失败";
                }
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常："+ex.Message;
            }

            return await Task.Run( () => res);

        }
    }
}