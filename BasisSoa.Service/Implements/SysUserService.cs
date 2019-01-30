using BasisSoa.Common.ClientData;
using BasisSoa.Common.EncryptionHelper;
using BasisSoa.Common.EnumHelper;
using BasisSoa.Core.Model.Sys;
using BasisSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
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
                var userInfo = Db.Queryable<SysUser, SysUserLogon>((sysuser, userlog) => sysuser.Id == userlog.UserId)
                   .Where((sysuser, userlog) => sysuser.Account == username)
                   .Select((sysuser, userlog) => new
                    {
                       sysuser,
                        userlog
                    }).First();

                if (userInfo != null)
                {
                    password = Md5Crypt.Encrypt(DES3Encrypt.EncryptString(password.ToLower(), userInfo.userlog.UserSecretkey).ToLower(), false).ToLower();
                    if (userInfo.userlog.UserPassword.Equals(password))
                    {
                        res.data = userInfo.sysuser;
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
            return await Task.Run(() => res);
        }
        /// <summary>
        /// 获取用户列表（带有自身权限）
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysUser>>> UserQueryAsync()
        {
            var res = new ApiResult<List<SysUser>>();

            try
            {
                
                var list = Db.Queryable<SysUser>()
                      .Mapper(it => it.sysOrganize, it => it.OrganizeId)
                      .Mapper(it => it.sysRole, it => it.RoleId)
                      .Mapper(it => it.sysUserLogon, it => it.sysUserLogon.UserId) //主表根据字表存的id来查
                      .ToList();

                res.data = list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return await Task.Run(() => res);
        }
    }
}
