using BasisSoa.Common.EnumHelper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasisSoa.Service.Interfaces
{
    public interface IBaseServer<TEntity> where TEntity : class
    {

        

        #region 添加 
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity model);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        Task<bool> AddListAsync(List<TEntity> parm);
        #endregion

        #region 删除 
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(object id);
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity model);


        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where);
        #endregion

        #region 修改 
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity model);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        Task<bool> UpdateListAsync(List<TEntity> parm);

        /// <summary>
        /// 根据条件更新多个数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, string strWhere);

        /// <summary>
        /// 根据条件更新多个数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where);
        #endregion

        #region  查询
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<TEntity> QueryByIDAsync(object objId);
        /// <summary>
        /// 根据主键查询 是否使用缓存
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="blnUseCache"></param>
        /// <returns></returns>
        Task<TEntity> QueryByIDAsync(object objId, bool blnUseCache = false);
        /// <summary>
        /// 根据主键列表查询
        /// </summary>
        /// <param name="lstIds"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryByIDsAsync(object[] lstIds);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync();

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> strOrderByFileds = null, OrderByType type = OrderByType.Desc);

        /// <summary>
        /// 根据条件查询一条
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<TEntity> QueryFirstAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件分页查询（带条件，页码，排序字段，排序方式）
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, Expression<Func<TEntity, object>> strOrderByFileds = null, OrderByType type = OrderByType.Desc);
        #endregion


    }
}
