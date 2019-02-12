using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BasisSoa.Common.AttributeHelper
{
    /// <summary>
    /// 查询泛型实体
    /// </summary>
    /// <typeparam name="T">数据源实体类</typeparam>
    /// <typeparam name="S">查询实体类</typeparam>
    public static class SearchClass<T, S>
        where T : class, new()
        where S : class
    {

        /// <summary>
        /// 获取where表达式
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression<Func<T, bool>> GetWhereLambda(S s1 = null)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "c");
            Expression Iwhere = null;

            bool HasValue = false;
            foreach (System.Reflection.PropertyInfo pinfo in s1.GetType().GetProperties())
            {

                if (!pinfo.CanRead)
                {
                    continue;
                }
                object Selvalue = pinfo.GetValue(s1, null);
                if (Selvalue == null)
                {
                    continue;
                }
                object ob = pinfo.GetCustomAttributes(typeof(SearchAttribute), false).FirstOrDefault();
                string TableName = null;
                string ColName = pinfo.Name;
                Symbol symbl = Symbol.等于;
                if (ob != null)
                {
                    SearchAttribute dA = (SearchAttribute)ob;
                    TableName = dA.TableName;
                    ColName = dA.BaseName;
                    symbl = dA.Symbol;
                }
                if (symbl == Symbol.无效)
                {
                    continue;
                }
                object op = pinfo.GetCustomAttributes(typeof(ExpressAttrInterface), true).FirstOrDefault();

                Expression left = null;
                if (op != null)
                {
                    left = (op as ExpressAttrInterface).GetExpression(param, typeof(T));
                }
                else
                {
                    if (string.IsNullOrEmpty(TableName))
                    {
                        left = Expression.Property(param, typeof(T).GetProperty(ColName));
                    }
                    else
                    {
                        left = Expression.Property(Expression.Property(param, typeof(T).GetProperty(TableName)), typeof(T).GetProperty(TableName).PropertyType.GetProperty(ColName));
                    }
                }
                Expression where2 = whereSymbol(symbl, left, null, Selvalue);

                if (Iwhere == null)
                {
                    Iwhere = where2;
                }
                else
                {
                    Iwhere = Expression.And(Iwhere, where2);
                }
                HasValue = true;
            }
            if (Iwhere == null || HasValue == false)
            {
                Iwhere = Expression.Constant(true);
            }
            return Expression.Lambda<Func<T, bool>>(Iwhere, param);
        }

        /// <summary>
        /// 规则条件
        /// </summary>
        /// <param name="symbl"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="Selvalue"></param>
        /// <returns></returns>
        public static Expression whereSymbol(Symbol symbl, Expression left, Expression right = null, object Selvalue = null)
        {
            Symbol vlsymbl = symbl;
            Expression where2 = null;


            if (right == null)
            {
                if (Symbol.空 == vlsymbl)
                {
                    bool IsNull = (bool)Selvalue;

                    right = Expression.Constant(null, left.Type);
                    if (IsNull)
                    {
                        vlsymbl = Symbol.等于;
                    }
                    else
                    {
                        vlsymbl = Symbol.不等于;
                    }
                }
                else
                {

                    if (Selvalue == null || BaseToolClass.GetObType(left.Type) == BaseToolClass.GetObType(Selvalue.GetType()))
                    {
                        right = Expression.Constant(Selvalue, left.Type);
                    }
                    else
                    {
                        right = Expression.Constant(Selvalue, Selvalue.GetType());
                    }
                }
            }
            switch (vlsymbl)
            {
                case Symbol.等于:
                    where2 = Expression.Equal(left, right);
                    break;
                case Symbol.大于等于:
                    where2 = Expression.GreaterThanOrEqual(left, right);
                    break;
                case Symbol.大于:
                    where2 = Expression.GreaterThan(left, right);
                    break;
                case Symbol.小于等于:
                    where2 = Expression.LessThanOrEqual(left, right);
                    break;
                case Symbol.小于:
                    where2 = Expression.LessThan(left, right);
                    break;
                case Symbol.相似:
                    where2 = Expression.Call(
                    left,
                    typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                    right);
                    break;
                case Symbol.包含:
                    if (Selvalue.GetType().IsArray)
                    {
                        where2 = Expression.Call(
                         null,
                         BaseToolClass.GetMethodInfo(typeof(Enumerable), "Contains", null, new Type[] { typeof(IEnumerable<TSource_1>), typeof(TSource_1) }, true).MakeGenericMethod(Selvalue.GetType().GetElementType()),
                         right,
                         left);
                    }
                    else
                    {
                        where2 = Expression.Call(
                        right,
                        Selvalue.GetType().GetMethod("Contains", new Type[] { Selvalue.GetType() }),
                        left);
                    };
                    break;
                case Symbol.不等于:
                    where2 = Expression.NotEqual(left, right);
                    break;
            }
            return where2;
        }
    }
}
