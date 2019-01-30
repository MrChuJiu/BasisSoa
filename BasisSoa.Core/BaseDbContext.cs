using BasisSoa.Core.Model.Sys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasisSoa.Core
{
    public class BaseDbContext
    {

        private static string _connectionString;
        private static DbType _dbType;
        private SqlSugarClient _db;

        /// <summary>
        /// 连接字符串 
        /// Blog.Core
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        /// <summary>
        /// 数据库类型 
        /// Blog.Core 
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }
        /// <summary>
        /// 数据连接对象 
        /// Blog.Core 
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }

        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// Blog.Core 
        /// </summary>
        public static BaseDbContext Context
        {
            get
            {
                return new BaseDbContext();
            }

        }


        /// <summary>
        /// 功能描述:构造函数
        /// </summary>
        private BaseDbContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException("数据库连接字符串为空");
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = true,
                IsShardSameThread = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                MoreSettings = new ConnMoreSettings()
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });
            //调式代码 用来打印SQL 
            _db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            try
            {
                //Db.CodeFirst.BackupTable().InitTables(typeof(SysLog), typeof(SysUser), typeof(SysUserLogon),
                //                                      typeof(SysOrganize), typeof(SysRole), typeof(SysModule),
                //                                      typeof(SysModuleAction), typeof(SysRoleAuthorize), typeof(SysRoleAuthorizeAction));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }



        /// <summary>
        /// 功能描述:设置初始化参数
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        /// <param name="enmDbType">数据库类型</param>
        public static void Init(string strConnectionString, DbType enmDbType = SqlSugar.DbType.SqlServer)
        {
            _connectionString = strConnectionString;
            _dbType = enmDbType;
        }
        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// </summary>
        /// <param name="db">db</param>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDB<T>(SqlSugarClient db) where T : class, new()
        {
            return new SimpleClient<T>(db);
        }
    }
}
