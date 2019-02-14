using BasisSoa.Common.ClientData;
using BasisSoa.Core.Model.Sys;
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


        Task<List<SysUser>> UserQueryAsync(Expression<Func<SysUser, bool>> whereExpression, int PageIndex, int PageSize, Expression<Func<SysUser, object>> strOrderByFileds = null, bool isAsc = true);


        Task<SysUser> QueryAsyncById(string Id);

    }
}
