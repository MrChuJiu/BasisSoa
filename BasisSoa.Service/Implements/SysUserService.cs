using BasisSoa.Common.ClientData;
using BasisSoa.Common.EncryptionHelper;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Implements
{
    public class SysUserService : BaseServer<SysUser>, ISysUserService
    {
        /// <summary>
        /// 根据用户名密码获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysUser>> UserNameAndPassQueryAsync(string username, string password)
        {
            var res = new ApiResult<SysUser>();
            res.data = new SysUser();

            try
            {

                var userInfo = await Db.Queryable<SysUser>()
                      .Where(s => s.Account == username)
                      .Mapper(it => it.sysUserLogon, it => it.sysUserLogon.UserId)
                      .FirstAsync();

                if (userInfo != null)
                {
                    password = Md5Crypt.Encrypt(DES3Encrypt.EncryptString(password.ToLower(), userInfo.sysUserLogon.UserSecretkey).ToLower(), false).ToLower();
                    if (userInfo.sysUserLogon.UserPassword.Equals(password))
                    {
                        res.data = userInfo;
                    }
                    else
                    {
                        res.code = (int)ApiEnum.Failure;
                        res.message = "账号错误，请重新输入";
                    }
                }
                else
                {
                    res.code = (int)ApiEnum.Failure;
                    res.message = "用户不存在";
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.code = (int)ApiEnum.Error;
            }
            return res;
        }
        /// <summary>
        /// 获取用户列表（带有自身权限）
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysUser>> UserQueryAsync(Expression<Func<SysUser, bool>> whereExpression, int PageIndex, int PageSize, Expression<Func<SysUser, object>> strOrderByFileds = null, OrderByType type = OrderByType.Desc)
        {
            var res = new List<SysUser>();
            try
            {

                res = await Db.Queryable<SysUser>()
                      .WhereIF(whereExpression != null, whereExpression)
                      .Mapper(it => it.sysUser, it => it.CreatorId)
                      .Mapper(it => it.sysOrganize, it => it.OrganizeId)
                      .Mapper(it => it.sysRole, it => it.RoleId)
                      .Mapper(it => it.sysUserLogon, it => it.sysUserLogon.UserId) //主表根据字表存的id来查
                      .OrderByIF(strOrderByFileds != null, strOrderByFileds,OrderByType.Desc)
                      .ToPageListAsync(PageIndex, PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return res;
        }
        /// <summary>
        /// 获取用户列表（带有自身权限）
        /// </summary>
        /// <returns></returns>
        public async Task<SysUser> QueryAsyncById(string Id)
        {
            var res = new SysUser();

            try
            {
                 res = await Db.Queryable<SysUser>()
                      .Where(s=>s.Id == Id)
                      .Mapper(it => it.sysOrganize, it => it.OrganizeId)
                      .Mapper(it => it.sysRole, it => it.RoleId)
                      .Mapper(it => it.sysUserLogon, it => it.sysUserLogon.UserId) //主表根据字表存的id来查
                      .FirstAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return res;
        }
    }
}
