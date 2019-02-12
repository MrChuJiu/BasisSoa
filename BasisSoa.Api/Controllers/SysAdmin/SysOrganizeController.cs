using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.Jwt;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Api.ViewModels.Sys.SysOrganize;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Common.TreeHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SysOrganizeController : Controller
    {

        private readonly ISysOrganizeService _sysOrganizeService;
        private readonly IMapper _mapper;
        public SysOrganizeController(IMapper mapper, ISysOrganizeService sysOrganizeService) {

            _mapper = mapper;
            _sysOrganizeService = sysOrganizeService;
        }


        /// <summary>
        /// 获取组织树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOrganizeTreeList")]
        public async Task<ApiResult<List<TreeListSysOrganizeDto>>> GetOrganizeTreeList()
        {
            ApiResult<List<TreeListSysOrganizeDto>> res = new ApiResult<List<TreeListSysOrganizeDto>>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                var OrganizeList = await _sysOrganizeService.QueryAsync();
                res.data = TreeGenerateTools.TreeGroup(_mapper.Map<List<TreeListSysOrganizeDto>>(OrganizeList), token.Organize);
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 根据组织Id获取 组织详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DetailsSysOrganizeDto>> Get(string Id) {
            ApiResult<DetailsSysOrganizeDto> res = new ApiResult<DetailsSysOrganizeDto>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                res.data = _mapper.Map<DetailsSysOrganizeDto>(await _sysOrganizeService.QuerySysOrganizeByIDAsync(Id));
            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);
        }

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Post(EditSysOrganizeDto Params) {
            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);


            try
            {
                SysOrganize sysOrganizeInfo = _mapper.Map<SysOrganize>(Params);
                sysOrganizeInfo.CreatorTime = DateTime.Now;
                sysOrganizeInfo.CreatorUserId = token.Id;
                sysOrganizeInfo.Id = Guid.NewGuid().ToString();
                sysOrganizeInfo.DeleteMark = false;
                var IsSuccess = await _sysOrganizeService.AddAsync(sysOrganizeInfo);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：添加组织失败";
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
        /// 修改组织
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<ApiResult<string>> Put(string Id, EditSysOrganizeDto Params) {

            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                SysOrganize sysOrganizeInfo = _mapper.Map<SysOrganize>(Params);
                sysOrganizeInfo.Id = Id;
                var IsSuccess = await _sysOrganizeService.UpdateAsync(sysOrganizeInfo);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：修改组织失败";
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
        /// 删除组织
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete(string Id)
        {
            ApiResult<string> res = new ApiResult<string>();
            TokenModelBeta token = JwtToken.ParsingJwtToken(HttpContext);

            try
            {
                var IsSuccess = await _sysOrganizeService.DeleteAsync(s=>s.Id == Id);
                if (!IsSuccess)
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "错误：删除组织失败";
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
