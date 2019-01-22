using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasisSoa.Api.ViewModels.Sys;
using BasisSoa.Common.ClientData;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Extensions.Jwt;
using BasisSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasisSoa.Api.Controllers.SysAdmin
{
    [Route("api/[controller]")]
    public class SysOrganizeController : Controller
    {

        private readonly ISysOrganizeService _sysOrganizeService;
        private readonly IMapper _mapper;
        public SysOrganizeController(IMapper mapper, ISysOrganizeService sysOrganizeService) {

            _mapper = mapper;
            _sysOrganizeService = sysOrganizeService;
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

                res.data = _mapper.Map<DetailsSysOrganizeDto>(_sysOrganizeService.QueryByIDAsync(Id));

            }
            catch (Exception ex)
            {
                res.code = (int)ApiEnum.Error;
                res.message = "异常：" + ex.Message;
            }

            return await Task.Run(() => res);
        }


        [HttpPost]
        public async Task<ApiResult<string>> Post() {
            ApiResult<string> res = new ApiResult<string>();
            return await Task.Run( () => res);
        }
 
    }
}
