using BasisSoa.Common.ClientData;
using BasisSoa.Core.Model.Sys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface ISysUserService : IBaseServer<SysUser>
    {
        /// <summary>
        /// 根据用户名密码获取用户信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        Task<ApiResult<SysUser>> UserNameAndPassQueryAsync(string username, string password);

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<SysUser>> UserQueryAsync(Expression<Func<SysUser, bool>> whereExpression, int PageIndex, int PageSize, Expression<Func<SysUser, object>> strOrderByFileds = null, OrderByType type = OrderByType.Desc);

        /// <summary>
        /// 根据用户Id获取用户详细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<SysUser> QueryAsyncById(string Id);

    }
}
