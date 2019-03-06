using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BasisSoa.Common.LambdaHelper
{
    public static class SearchClass<T, S, V>
        where T : class, new()
        where S : class
        where V : class, new()
    {
        /// <summary>
        /// 获取SelectLambda
        /// </summary>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression<Func<T, V>> GetSelectLambda(Expression<Func<T, V>> selectEx = null)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "c");
            Expression Iwhere = null;
            List<MemberBinding> mebers = new List<MemberBinding>();
            if (selectEx != null)
            {
                param = selectEx.Parameters.FirstOrDefault();
                mebers = ((MemberInitExpression)selectEx.Body).Bindings.ToList();
            }
            System.Linq.Expressions.NewExpression newAnimal =
                        System.Linq.Expressions.Expression.New(typeof(V));

            foreach (System.Reflection.PropertyInfo pinfo in typeof(V).GetProperties())
            {
                try
                {
                    if (mebers.Exists(s => s.Member.Name == pinfo.Name))
                    {
                        continue;
                    }
                    Expression valueEx = null;
                    object ob = pinfo.GetCustomAttributes(typeof(DataAttribute), false).FirstOrDefault();
                    object ea = pinfo.GetCustomAttributes(typeof(ChildTableAttribute), false).FirstOrDefault();
                    object opAttr = pinfo.GetCustomAttributes(typeof(OperationAttribute), false).FirstOrDefault();
                    System.Reflection.PropertyInfo TableInfo = null;
                    if (ea != null)
                    {
                        valueEx = (ea as ChildTableAttribute).GetExpression(param, typeof(T));
                        TableInfo = (ea as ChildTableAttribute).getPropertyInfo(typeof(T));
                    }

                    MemberBinding Select = null;

                    if (ob != null || (opAttr != null && valueEx != null))
                    {
                        #region 存在子实体数据
                        //存在子实体数据
                        DataAttribute dataAtt = (DataAttribute)ob;
                        System.Reflection.MemberInfo Minfo = null;


                        if (TableInfo == null)
                        {
                            if (dataAtt.TableName != null)
                            {
                                TableInfo = typeof(T).GetProperty(dataAtt.TableName);
                            }
                            else
                            {
                                // 当前表别名处理无子从表
                                valueEx = Expression.Property(param, dataAtt.ColName);
                                if (valueEx.Type != pinfo.PropertyType)
                                {
                                    valueEx = Expression.Convert(valueEx, pinfo.PropertyType);
                                }
                                Select = Expression.Bind(pinfo, valueEx);
                                mebers.Add(Select);
                                continue;
                            }
                        }

                        //获取外键表类映射
                        if (TableInfo.PropertyType.IsGenericType && TableInfo.PropertyType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ICollection<>))
                        {
                            #region 子从表外键
                            // 如果外键表是子从表
                            object op = pinfo.GetCustomAttributes(typeof(OperAttribute), false).FirstOrDefault();
                            if (op != null)
                            {
                                // 条件运算
                                OperAttribute opAtt = (OperAttribute)op;
                                // 统计表
                                Type tableType = BaseToolClass.GetObType(TableInfo.PropertyType);

                                // 分表实体 
                                ParameterExpression paramd = Expression.Parameter(tableType, "d");
                                Expression whereRight = null;

                                if (ea != null)
                                {
                                    if (opAttr == null)
                                    {
                                        whereRight = (ob as ExpressAttrInterface).GetExpression(paramd, tableType);

                                    }
                                    else
                                    {
                                        whereRight = (opAttr as ExpressAttrInterface).GetExpression(paramd, tableType);
                                    }
                                }
                                else
                                {
                                    whereRight = Expression.Property(paramd, tableType.GetProperty(dataAtt.ColName));
                                }
                                // 筛选检索条件where
                                object[] otList = pinfo.GetCustomAttributes(typeof(OtherKeyAttribute), false);
                                Expression whereAll = null;
                                foreach (object ot in otList)
                                {
                                    OtherKeyAttribute otAtt = (OtherKeyAttribute)ot;
                                    MemberExpression whereLeft = Expression.Property(paramd, tableType.GetProperty(otAtt.CkColName));
                                    Expression whereZ = whereSymbol(otAtt.Symbol, whereLeft, null, otAtt.values);
                                    if (whereAll == null)
                                    {
                                        whereAll = whereZ;
                                    }
                                    else
                                    {
                                        whereAll = Expression.And(Iwhere, whereZ);
                                    }
                                }
                                //object otList1 = pinfo.GetCustomAttributes(typeof(SearchKeyAttribute), false);
                                //if (otList1 != null)
                                //{
                                //    SearchKeyAttribute otAtt = (SearchKeyAttribute)otList1;
                                //    int num = otAtt.Num;
                                //    for (int i = 0; i < num; i++)
                                //    {
                                //        MemberExpression whereLeft = Expression.Property(paramd, tableType.GetProperty(otAtt.ColName[i]));
                                //        Expression whereZ = whereSymbol(otAtt.Symbol[i], whereLeft, null, otAtt.Values[i]);
                                //        if (whereAll == null)
                                //        {
                                //            whereAll = whereZ;
                                //        }
                                //        else
                                //        {
                                //            whereAll = Expression.And(Iwhere, whereZ);
                                //        }
                                //    }
                                //}

                                System.Reflection.MethodInfo methInfo = BaseToolClass.GetMethodInfo(typeof(Expression), "Lambda", null, new Type[] { typeof(Expression), typeof(ParameterExpression[]) }, true);
                                object Value = methInfo.MakeGenericMethod(typeof(Func<,>).MakeGenericType(tableType, typeof(bool))).Invoke(null, new object[] { whereAll, new System.Linq.Expressions.ParameterExpression[] { paramd } });
                                Expression whereTable = valueEx;

                                var WhereExpression = Expression.Lambda(whereAll, paramd);
                                var resultExpression = Expression.Call(null,
                                    BaseToolClass.GetMethodInfo(typeof(Enumerable), "Where", null, new Type[] { typeof(IEnumerable<TSource_1>), typeof(Func<TSource_1, bool>) }, true).MakeGenericMethod(tableType)
                                    , whereTable, WhereExpression);

                                // 统计属性
                                Expression syValue = null;

                                if (whereRight.Type != pinfo.PropertyType)
                                {
                                    whereRight = Expression.Convert(whereRight, pinfo.PropertyType);
                                }
                                syValue = Expression.Lambda(whereRight, paramd);


                                Type OType = pinfo.PropertyType;
                                if (opAtt.Symol == OperSymol.Count)
                                {
                                    OType = typeof(int);
                                }
                                whereTable = Expression.Call(
                                         null,
                                         BaseToolClass.GetMethodInfo(typeof(Enumerable), opAtt.ToMethodStr(), null, new Type[] { typeof(IEnumerable<TSource_1>), typeof(Func<,>).MakeGenericType(typeof(TSource_1), OType) }, true, OType).MakeGenericMethod(tableType),
                                         resultExpression,
                                         syValue
                                    );
                                Minfo = pinfo;
                                valueEx = whereTable;
                                Select = Expression.Bind(Minfo, valueEx);
                                mebers.Add(Select);
                            }

                            continue;
                            #endregion

                        }
                        else
                        {
                            // 如果外键表是外键关联表

                            if (valueEx == null)
                            {
                                valueEx = (ob as ExpressAttrInterface).GetExpression(param, typeof(T));
                            }
                            else
                            {

                                if (opAttr == null)
                                {
                                    if (ea != null)
                                    {
                                        valueEx = (ob as ExpressAttrInterface).GetExpression(valueEx, valueEx.Type);
                                    }
                                    else
                                    {
                                        valueEx = Expression.Property(valueEx, dataAtt.ColName);
                                    }
                                }
                                else
                                {
                                    valueEx = (opAttr as ExpressAttrInterface).GetExpression(valueEx, valueEx.Type);

                                }

                            }
                            Minfo = pinfo;
                        }
                        if (valueEx.Type != pinfo.PropertyType)
                        {
                            valueEx = Expression.Convert(valueEx, pinfo.PropertyType);
                        }
                        Select = Expression.Bind(Minfo, valueEx);
                        mebers.Add(Select);
                        #endregion
                    }
                    else
                    {
                        if (opAttr != null)
                        {

                            if (valueEx == null)
                            {
                                valueEx = (opAttr as ExpressAttrInterface).GetExpression(param, param.Type);
                            }
                            else
                            {
                                valueEx = (opAttr as ExpressAttrInterface).GetExpression(valueEx, valueEx.Type);
                            }
                        }
                        else
                        {
                            if (valueEx == null)
                            {
                                valueEx = Expression.Property(param, pinfo.Name);
                            }
                            else
                            {
                                valueEx = Expression.Property(valueEx, pinfo.Name);
                            }
                        }
                        if (valueEx.Type != typeof(V).GetProperty(pinfo.Name).PropertyType)
                        {
                            valueEx = Expression.Convert(valueEx, typeof(V).GetProperty(pinfo.Name).PropertyType);
                        }
                        mebers.Add(Expression.Bind(typeof(V).GetProperty(pinfo.Name), valueEx));
                    }
                }
                catch { }
            }
            Expression sel = Expression.MemberInit(newAnimal, mebers);
            return Expression.Lambda<Func<T, V>>(sel, param);
        }


        /// <summary>
        /// 获取where表达式
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression<Func<T, bool>> GetWhereLambda(S s1 = null, System.Linq.Expressions.Expression<Func<T, bool>> ExAdd = null)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "c");
            Expression Iwhere = null;
            if (ExAdd != null)
            {
                param = ExAdd.Parameters[0];
                Iwhere = ExAdd.Body;
            }
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
