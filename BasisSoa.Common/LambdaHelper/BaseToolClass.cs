using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace BasisSoa.Common.LambdaHelper
{
    public static class BaseToolClass
    {

        /// <summary>
        /// 获取数据实体是否被其他表使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CheckDateIsUseing(object model)
        {
            System.Reflection.PropertyInfo[] filed = model.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo file in filed)
            {
                if (file.PropertyType.IsGenericType && file.PropertyType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ICollection<>))
                {
                    object value = file.GetValue(model);
                    object count = value.GetType().GetProperty("Count").GetValue(value);
                    if (count == null || count.ToString() == "0")
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }




        /// <summary>
        /// 获取泛型实体类
        /// </summary>
        /// <param name="itype"></param>
        /// <returns></returns>
        public static Type GetObType(Type itype)
        {
            Type oType = itype;
            if (oType.IsGenericType)
            {
                oType = GetObType(oType.GenericTypeArguments[0]);
            }
            return oType;
        }


        /// <summary>
        /// 获取子项实体
        /// </summary>
        /// <param name="InOb"></param>
        /// <param name="ProName"></param>
        /// <returns></returns>
        public static object GetModelValue(object InOb, string ProName)
        {
            System.Reflection.PropertyInfo pinfo = GetObType(InOb.GetType()).GetProperty(ProName);
            if (pinfo != null)
            {
                return pinfo.GetValue(InOb);
            }
            return null;
        }

        public static object GetModelValue(object InOb, string TableNameOb, string ProName)
        {
            System.Reflection.PropertyInfo pinfo = GetObType(InOb.GetType()).GetProperty(TableNameOb);
            if (pinfo != null)
            {
                object tableob = pinfo.GetValue(InOb);
                object value = GetObType(tableob.GetType()).GetProperty(ProName).GetValue(tableob);
                return value;
            }
            return null;

        }


        public static System.Reflection.MethodInfo GetMethodInfo(Type Itype, string Name, System.Reflection.BindingFlags? flags = null, Type[] parmTyps = null, bool? ContainsGeneric = null, Type ReturnType = null)
        {

            System.Reflection.MethodInfo[] mths = null;
            if (flags == null)
            {
                mths = Itype.GetMethods();
            }
            else
            {
                mths = Itype.GetMethods((System.Reflection.BindingFlags)flags);
            }
            if (mths != null)
            {
                foreach (System.Reflection.MethodInfo meth in mths)
                {
                    if (ReturnType != null)
                    {
                        if (meth.ReturnType != ReturnType)
                        {
                            continue;
                        }
                    }
                    if (meth.Name == Name)
                    {
                        if (parmTyps != null)
                        {

                            System.Reflection.ParameterInfo[] infos = meth.GetParameters();
                            if (parmTyps.Length != infos.Length)
                            {
                                continue;
                            }
                            if (infos.Equals(parmTyps))
                            {
                                return meth;
                            }
                            Type[] parmTypes = meth.GetGenericArguments();
                            Type[] ChangeTypes = GenericTypeChange(parmTyps, parmTypes);

                            bool Result = true;
                            for (int i = 0; i < ChangeTypes.Length; i++)
                            {
                                if (infos[i].ParameterType == ChangeTypes[i])
                                {
                                    continue;
                                }
                                if (infos[i].ParameterType.IsConstructedGenericType)
                                {
                                    if (infos[i].ParameterType.GetGenericTypeDefinition() == ChangeTypes[i].GetGenericTypeDefinition())
                                    {
                                        continue;
                                    }
                                }
                                Result = false;
                            }
                            if (Result == false)
                            {
                                continue;
                            }
                        }
                        if (ContainsGeneric != null)
                        {
                            if (meth.ContainsGenericParameters != ContainsGeneric)
                            {
                                continue;
                            }
                        }
                        return meth;
                    }
                }
            }
            return null;
        }
        private static Type[] GenericTypeChange(Type[] parmTyps, Type[] GenericType)
        {
            List<Type> reulset = new List<Type>();
            foreach (Type a in parmTyps)
            {
                Type A = null;
                if (a == typeof(TSource_1))
                {
                    A = GenericType[0];
                }
                else if (a == typeof(TSource_2))
                {
                    A = GenericType[2];
                }
                else if (a.IsGenericType && a.GetGenericArguments() != null)
                {

                    A = a.GetGenericTypeDefinition().MakeGenericType(GenericTypeChange(a.GetGenericArguments(), GenericType));
                }
                else
                {
                    A = a;
                }
                reulset.Add(A);
            }
            return reulset.ToArray();
        }
        public static Type[] GetMethodType(Type Itype, string Name, System.Reflection.BindingFlags? flags = null, Type[] parmTyps = null, bool? ContainsGeneric = null)
        {

            System.Reflection.MethodInfo[] mths = null;
            if (flags == null)
            {
                mths = Itype.GetMethods();
            }
            else
            {
                mths = Itype.GetMethods((System.Reflection.BindingFlags)flags);
            }
            if (mths != null)
            {
                foreach (System.Reflection.MethodInfo meth in mths)
                {
                    if (meth.Name == Name)
                    {
                        if (parmTyps != null)
                        {
                            System.Reflection.ParameterInfo[] infos = meth.GetParameters();
                            if (parmTyps.Length != infos.Length)
                            {
                                continue;
                            }
                            bool Result = true;

                            for (int i = 0; i < parmTyps.Length; i++)
                            {
                                if (infos[i].ParameterType == parmTyps[i])
                                {
                                    continue;
                                }
                                if (infos[i].ParameterType.IsConstructedGenericType)
                                {
                                    if (infos[i].ParameterType.GetGenericTypeDefinition() == parmTyps[i].GetGenericTypeDefinition())
                                    {
                                        continue;
                                    }
                                }


                                Result = false;

                            }
                            if (Result == false)
                            {
                                continue;
                            }
                        }
                        if (ContainsGeneric != null)
                        {
                            if (meth.ContainsGenericParameters != ContainsGeneric)
                            {
                                continue;
                            }
                        }
                        System.Reflection.ParameterInfo[] pams = meth.GetParameters();
                        Type[] types = new Type[pams.Length];
                        for (int i = 0; i < pams.Length; i++)
                        {
                            types[i] = pams[i].ParameterType;
                        }
                        return types;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获取string转换值
        /// </summary>
        /// <param name="text"></param>
        /// <param name="itype"></param>
        /// <returns></returns>
        public static object GetPriseText(string text, Type itype)
        {
            Type obType = GetObType(itype);
            Type[] types = new Type[1];
            System.Reflection.ParameterModifier[] par1 = new System.Reflection.ParameterModifier[1];
            par1[0] = new System.Reflection.ParameterModifier(1);
            types[0] = typeof(string);
            System.Reflection.MethodInfo MethInfo = obType.GetMethod("Parse", types, par1);
            object[] value = new object[1];
            value[0] = text;
            try
            {
                if (MethInfo != null)
                {
                    return MethInfo.Invoke(null, value);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 实体类数据传输
        /// </summary>
        /// <param name="imodel"></param>
        /// <param name="omodel"></param>
        /// <returns></returns>
        public static object ModelChange(object imodel, object omodel)
        {
            System.Reflection.PropertyInfo[] propertyInfos = imodel.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo pinfo in propertyInfos)
            {
                if (!pinfo.CanRead)
                {
                    continue;
                }
                System.Reflection.PropertyInfo Oinfo = omodel.GetType().GetProperty(pinfo.Name);
                if (Oinfo == null)
                {
                    continue;
                }
                if (!Oinfo.CanWrite)
                {
                    continue;
                }
                if (pinfo.PropertyType.IsGenericType && pinfo.PropertyType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ICollection<>))
                {
                    string C = string.Empty;
                }
                if (pinfo.PropertyType.IsValueType)
                {
                    try
                    {

                        if (GetObType(Oinfo.PropertyType) == GetObType(pinfo.PropertyType))
                        {
                            Oinfo.SetValue(omodel, pinfo.GetValue(imodel));
                        }
                        else
                        {

                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        if (GetObType(Oinfo.PropertyType) == GetObType(pinfo.PropertyType))
                        {
                            Oinfo.SetValue(omodel, pinfo.GetValue(imodel));
                        }
                        else
                        {

                        }
                    }
                    catch { }

                }
            }
            return omodel;
        }
        /// <summary>
        /// 实体类数据传输
        /// </summary>
        /// <param name="imodel"></param>
        /// <param name="omodel"></param>
        /// <returns></returns>
        public static object ModelChangeNoForeignKey(object imodel, object omodel, string[] NameList)
        {
            System.Reflection.PropertyInfo[] propertyInfos = imodel.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo pinfo in propertyInfos)
            {
                if (!pinfo.CanRead)
                {
                    continue;
                }
                System.Reflection.PropertyInfo Oinfo = omodel.GetType().GetProperty(pinfo.Name);
                if (Oinfo == null)
                {
                    continue;
                }
                if (!Oinfo.CanWrite)
                {
                    continue;
                }
                if (NameList.Contains(pinfo.Name))
                {
                    continue;
                }


                if (pinfo.PropertyType.IsGenericType && pinfo.PropertyType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ICollection<>))
                {
                    string C = string.Empty;
                }
                if (pinfo.PropertyType.IsValueType)
                {
                    try
                    {

                        if (GetObType(Oinfo.PropertyType) == GetObType(pinfo.PropertyType))
                        {
                            Oinfo.SetValue(omodel, pinfo.GetValue(imodel));
                        }
                        else
                        {

                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        if (GetObType(Oinfo.PropertyType) == GetObType(pinfo.PropertyType))
                        {
                            Oinfo.SetValue(omodel, pinfo.GetValue(imodel));
                        }
                        else
                        {

                        }
                    }
                    catch { }

                }
            }
            return omodel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imodel"></param>
        /// <param name="OType"></param>
        /// <returns></returns>
        public static object GetModel(object imodel, object omodel)
        {

            System.Reflection.PropertyInfo[] propertyInfos = omodel.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo pinfo in propertyInfos)
            {
                try
                {
                    //获取异步表名
                    object ob = pinfo.GetCustomAttributes(typeof(DataAttribute), false).FirstOrDefault();

                    if (ob != null)
                    {
                        //存在子实体数据
                        DataAttribute dataAtt = (DataAttribute)ob;
                        // 该类型是从表
                        if (pinfo.PropertyType.IsGenericType && pinfo.PropertyType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.ICollection<>))
                        {
                            string C = string.Empty;
                        }
                        object Tablevalue = omodel.GetType().GetProperty(dataAtt.TableName).GetValue(imodel);
                        object value = Tablevalue.GetType().GetProperty(dataAtt.ColName).GetValue(Tablevalue);
                        pinfo.SetValue(omodel, value);

                    }
                    else
                    {

                        System.Reflection.PropertyInfo iinfo = imodel.GetType().GetProperty(pinfo.Name);
                        if (BaseToolClass.GetObType(iinfo.PropertyType) == BaseToolClass.GetObType(pinfo.PropertyType))
                        {
                            pinfo.SetValue(omodel, iinfo.GetValue(imodel));
                        }

                    }
                }
                catch { }
            }
            return omodel;
        }
        /// <summary>
        /// 数值抓数据列
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public static string NumbertoString(int colIndex)
        {
            string strResult = "";
            int once = colIndex / 26;
            int twice = colIndex % 26;
            strResult = ((char)(twice - 1 + 'A')).ToString() + strResult;
            if (once > 26)
            {
                NumbertoString(once);
            }
            return strResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="SumCol"></param>
        /// <returns></returns>
        public static Dictionary<string, decimal> ToGroup<TSource>(this IEnumerable<TSource> source, string key, string SumCol)
        {
            string KeyStr = key;
            Dictionary<string, decimal> grpList = new Dictionary<string, decimal>();
            IEnumerable<IGrouping<string, TSource>> GrList = null;
            if (KeyStr.IndexOf('.') > 0)
            {
                GrList = source.GroupBy(s => typeof(TSource).GetProperty(key.Split('.')[0]).GetValue(s) == null ? "" : typeof(TSource).GetProperty(key.Split('.')[0]).PropertyType.GetProperty(key.Split('.')[1]).GetValue(typeof(TSource).GetProperty(key.Split('.')[0]).GetValue(s)).ToString());
            }
            else
            {
                GrList =
                    source.GroupBy(s => typeof(TSource).GetProperty(key.Split('.')[0]).GetValue(s) == null ? "其他" : typeof(TSource).GetProperty(key.Split('.')[0]).GetValue(s).ToString());
            }
            var List = GrList.Select(s => new
            {
                Key = s.Key,
                Value = s.Sum(a => decimal.Parse((typeof(TSource).GetProperty(SumCol).GetValue(a) == null) ? "0" : (typeof(TSource).GetProperty(SumCol).GetValue(a)).ToString()))
            }).ToList();

            foreach (var t in List)
            {
                grpList.Add(t.Key, t.Value);
            }
            return grpList;
        }
        public static System.Data.DataTable SelectXDListToTable<TSource>(this List<TSource> source, bool isStoreDB = true)
        {
            Type tp = typeof(TSource);
            System.Reflection.PropertyInfo[] proInfos = tp.GetProperties();
            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (var item in proInfos)
            {
                Type colType = item.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                dt.Columns.Add(new System.Data.DataColumn(item.Name, colType)); //添加列明及对应类型  
            }
            foreach (var item in source)
            {
                System.Data.DataRow dr = dt.NewRow();
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item);
                    if (obj == null)
                    {
                        continue;
                    }
                    if (isStoreDB && proInfo.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    dr[proInfo.Name] = obj;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #region 查询扩展
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <typeparam name="T">数据源</typeparam>
        /// <typeparam name="S">查询实体</typeparam>
        /// <param name="sourse"></param>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereFrom<T, S>(this IQueryable<T> sourse, S s1, System.Linq.Expressions.Expression<Func<T, bool>> whereAdd = null)
            where T : class, new()
            where S : class
        {
            System.Linq.Expressions.Expression<Func<T, bool>> where = SearchClass<T, S, T>.GetWhereLambda(s1, whereAdd);
            return sourse.Where(where);
        }
        /// <summary>
        /// 前台类型获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="sourse"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public static IQueryable<V> SelectTo<T, V>(this IQueryable<T> sourse, V view)
            where V : class, new()
            where T : class, new()
        {
            System.Linq.Expressions.Expression<Func<T, V>> select = SearchClass<T, T, V>.GetSelectLambda();
            return sourse.Select(select);
        }
        /// <summary>
        /// 前台类型获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="sourse"></param>
        /// <param name="selectAdd">优先赋值项目</param>
        /// <returns></returns>
        public static IQueryable<V> SelectTo<T, V>(this IQueryable<T> sourse, System.Linq.Expressions.Expression<Func<T, V>> selectAdd = null)
            where V : class, new()
            where T : class, new()
        {
            System.Linq.Expressions.Expression<Func<T, V>> select = SearchClass<T, T, V>.GetSelectLambda(selectAdd);
            return sourse.Select(select);
        }
        public static List<V> Search<T, S, V>(this IQueryable<T> sourse, V view, S s1)
            where V : class, new()
            where S : class
            where T : class, new()
        {
            return sourse.Search_Query(view).ToList();
        }
        public static List<V> Search<T, V>(this IQueryable<T> sourse, System.Linq.Expressions.Expression<Func<T, V>> selectAdd = null)
            where V : class, new()
            where T : class, new()
        {
            return sourse.Search_Query(selectAdd).ToList();
        }
        public static IQueryable<V> Search_Query<T, V>(this IQueryable<T> sourse, V view)
            where V : class, new()
            where T : class, new()
        {
            T A = null;
            return Search_Query(sourse, view, A);
        }
        public static IQueryable<V> Search_Query<T, V>(this IQueryable<T> sourse, System.Linq.Expressions.Expression<Func<T, V>> selectAdd = null)
            where V : class, new()
            where T : class, new()
        {
            T A = null;
            return Search_Query(sourse, selectAdd, A);
        }
        public static IQueryable<V> Search_Query<T, S, V>(this IQueryable<T> sourse, V view, S s1)
            where V : class, new()
            where S : class
            where T : class, new()
        {
            IQueryable<T> whereQuery = null;
            if (s1 != null)
            {
                System.Linq.Expressions.Expression<Func<T, bool>> where = SearchClass<T, S, V>.GetWhereLambda(s1);
                whereQuery = whereQuery.Where(where);
            }
            System.Linq.Expressions.Expression<Func<T, V>> select = SearchClass<T, S, V>.GetSelectLambda();
            return sourse.Select(select);
        }
        public static IQueryable<V> Search_Query<T, S, V>(this IQueryable<T> sourse, System.Linq.Expressions.Expression<Func<T, V>> selectAdd, S s1)
            where V : class, new()
            where S : class
            where T : class, new()
        {
            IQueryable<T> whereQuery = null;
            if (s1 != null)
            {
                System.Linq.Expressions.Expression<Func<T, bool>> where = SearchClass<T, S, V>.GetWhereLambda(s1);
                whereQuery = whereQuery.Where(where);
            }
            System.Linq.Expressions.Expression<Func<T, V>> select = SearchClass<T, S, V>.GetSelectLambda(selectAdd);
            return sourse.Select(select);
        }
        # endregion


        #region 排序扩展
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string colStr, bool frist = true)
        {
            ParameterExpression param = Expression.Parameter(typeof(TSource), "c");
            bool asc = true;
            if (colStr.ToUpper().EndsWith(" DESC"))
            {
                asc = false;
                colStr = colStr.Remove(colStr.Length - 5).Trim();
            }
            List<string> collist = colStr.Split('.').ToList();
            Expression value = getChildExpression(param, collist);
            return (IOrderedQueryable<TSource>)(typeof(BaseToolClass).GetMethod("pvOrderBy").MakeGenericMethod(typeof(TSource), value.Type).Invoke(null, new object[] { source, param, value, asc, frist }));
            //return OrderBy(source,param, value, asc, frist);
        }
        public static IOrderedQueryable<TSource> OrderByList<TSource>(this IQueryable<TSource> source, List<string> colStrs, bool frist = false)
        {
            string colStr;
            colStr = colStrs[0];
            if (colStrs.Count == 1)
            {
                return source.OrderBy(colStr, frist);
            }
            colStrs.RemoveAt(0);
            //string colStrs
            return OrderByList(source.OrderBy(colStr, frist), colStrs);
        }


        private static Expression getChildExpression(Expression parent, List<string> parentList)
        {
            if (parentList.Count == 1)
            {
                return Expression.Property(parent, parentList[0]);
            }
            else
            {
                Expression chilid = Expression.Property(parent, parentList[0]);
                parentList.RemoveAt(0);
                return getChildExpression(chilid, parentList);
            }
        }
        public static IOrderedQueryable<TSource> pvOrderBy<TSource, TKey>(IQueryable<TSource> source, ParameterExpression param, Expression value, bool Asc = true, bool First = true)
        {
            Expression<Func<TSource, TKey>> resule = Expression.Lambda<Func<TSource, TKey>>(value, param);

            if (Asc)
            {
                if (First == false)
                {
                    return (source as IOrderedQueryable<TSource>).ThenBy(resule);
                }
                else
                {
                    return source.OrderBy(resule);
                }

            }
            else
            {
                if (First == false)
                {
                    return (source as IOrderedQueryable<TSource>).ThenByDescending(resule);
                }
                else
                {
                    return source.OrderByDescending(resule);
                }
            }
        }
        #endregion

        public static IQueryable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
             this IQueryable<TOuter> outer,
             IQueryable<TInner> inner,
             Expression<Func<TOuter, TKey>> outerKeySelector,
             Expression<Func<TInner, TKey>> innerKeySelector,
             Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {

            // generic methods
            var selectManies = typeof(Queryable).GetMethods()
                .Where(x => x.Name == "SelectMany" && x.GetParameters().Length == 3)
                .OrderBy(x => x.ToString().Length)
                .ToList();
            var selectMany = selectManies.First();
            var select = typeof(Queryable).GetMethods().First(x => x.Name == "Select" && x.GetParameters().Length == 2);
            var where = typeof(Queryable).GetMethods().First(x => x.Name == "Where" && x.GetParameters().Length == 2);
            var groupJoin = typeof(Queryable).GetMethods().First(x => x.Name == "GroupJoin" && x.GetParameters().Length == 5);
            var defaultIfEmpty = typeof(Queryable).GetMethods().First(x => x.Name == "DefaultIfEmpty" && x.GetParameters().Length == 1);

            // need anonymous type here or let's use Tuple
            // prepares for:
            // var q2 = Queryable.GroupJoin(db.A, db.B, a => a.Id, b => b.IdA, (a, b) => new { a, groupB = b.DefaultIfEmpty() });
            var tuple = typeof(Tuple<,>).MakeGenericType(
                typeof(TOuter),
                typeof(IQueryable<>).MakeGenericType(
                    typeof(TInner)
                    )
                );
            var paramOuter = Expression.Parameter(typeof(TOuter));
            var paramInner = Expression.Parameter(typeof(IEnumerable<TInner>));
            var groupJoinExpression = Expression.Call(
                null,
                groupJoin.MakeGenericMethod(typeof(TOuter), typeof(TInner), typeof(TKey), tuple),
                new Expression[]
                    {
                    Expression.Constant(outer),
                    Expression.Constant(inner),
                    outerKeySelector,
                    innerKeySelector,
                    Expression.Lambda(
                        Expression.New(
                            tuple.GetConstructor(tuple.GetGenericArguments()),
                            new Expression[]
                                {
                                    paramOuter,
                                    Expression.Call(
                                        null,
                                        defaultIfEmpty.MakeGenericMethod(typeof (TInner)),
                                        new Expression[]
                                            {
                                                Expression.Convert(paramInner, typeof (IQueryable<TInner>))
                                            }
                                )
                                },
                            tuple.GetProperties()
                            ),
                        new[] {paramOuter, paramInner}
                )
                    }
                );

            // prepares for:
            // var q3 = Queryable.SelectMany(q2, x => x.groupB, (a, b) => new { a.a, b });
            var tuple2 = typeof(Tuple<,>).MakeGenericType(typeof(TOuter), typeof(TInner));
            var paramTuple2 = Expression.Parameter(tuple);
            var paramInner2 = Expression.Parameter(typeof(TInner));
            var paramGroup = Expression.Parameter(tuple);
            var selectMany1Result = Expression.Call(
                null,
                selectMany.MakeGenericMethod(tuple, typeof(TInner), tuple2),
                new Expression[]
                    {
                    groupJoinExpression,
                    Expression.Lambda(
                        Expression.Convert(Expression.MakeMemberAccess(paramGroup, tuple.GetProperty("Item2")),
                                           typeof (IEnumerable<TInner>)),
                        paramGroup
                ),
                    Expression.Lambda(
                        Expression.New(
                            tuple2.GetConstructor(tuple2.GetGenericArguments()),
                            new Expression[]
                                {
                                    Expression.MakeMemberAccess(paramTuple2, paramTuple2.Type.GetProperty("Item1")),
                                    paramInner2
                                },
                            tuple2.GetProperties()
                            ),
                        new[]
                            {
                                paramTuple2,
                                paramInner2
                            }
                )
                    }
                );

            // prepares for final step, combine all expressinos together and invoke:
            // var q4 = Queryable.SelectMany(db.A, a => q3.Where(x => x.a == a).Select(x => x.b), (a, b) => new { a, b });
            var paramTuple3 = Expression.Parameter(tuple2);
            var paramTuple4 = Expression.Parameter(tuple2);
            var paramOuter3 = Expression.Parameter(typeof(TOuter));
            var selectManyResult2 = selectMany
                .MakeGenericMethod(
                    typeof(TOuter),
                    typeof(TInner),
                    typeof(TResult)
                )
                .Invoke(
                    null,
                    new object[]
                        {
                        outer,
                        Expression.Lambda(
                            Expression.Convert(
                                Expression.Call(
                                    null,
                                    select.MakeGenericMethod(tuple2, typeof(TInner)),
                                    new Expression[]
                                        {
                                            Expression.Call(
                                                null,
                                                where.MakeGenericMethod(tuple2),
                                                new Expression[]
                                                    {
                                                        selectMany1Result,
                                                        Expression.Lambda(
                                                            Expression.Equal(
                                                                paramOuter3,
                                                                Expression.MakeMemberAccess(paramTuple4, paramTuple4.Type.GetProperty("Item1"))
                                                            ),
                                                            paramTuple4
                                                        )
                                                    }
                                            ),
                                            Expression.Lambda(
                                                Expression.MakeMemberAccess(paramTuple3, paramTuple3.Type.GetProperty("Item2")),
                                                paramTuple3
                                            )
                                        }
                                ),
                                typeof(IEnumerable<TInner>)
                            ),
                            paramOuter3
                        ),
                        resultSelector
                        }
                );

            return (IQueryable<TResult>)selectManyResult2;
        }
        public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
        {
            private Func<T, V> keySelector;

            public CommonEqualityComparer(Func<T, V> keySelector)
            {
                this.keySelector = keySelector;
            }

            public bool Equals(T x, T y)
            {
                return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
            }
        }
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

    }
    public class TSource_1
    {

    }
    public class TSource_2
    {

    }
}
