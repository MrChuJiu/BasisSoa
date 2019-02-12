using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BasisSoa.Common.AttributeHelper
{
    public class ChildTableAttribute : Attribute, ExpressAttrInterface
    {
        public ChildTableAttribute(string TableName, object ChildName)
        {
            this.ColName = ExpressAttrCommon.getExpressAttr(ChildName);
            this.TableName = TableName;
        }

        public string TableName;

        public object ColName;
        /// <summary>
        /// 获取子表Expression
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="TableType"></param>
        /// <returns></returns>
        public Expression GetExpression(Expression parm, Type TableType)
        {

            if (ColName is ExpressAttrInterface)
            {
                if (string.IsNullOrEmpty(TableName))
                {
                    return (ColName as ExpressAttrInterface).GetExpression(parm, TableType);
                }
                else
                {
                    return (ColName as ExpressAttrInterface).GetExpression(Expression.Property(parm, TableName), TableType.GetProperty(TableName).PropertyType);
                }


            }
            else
            {
                if (string.IsNullOrEmpty(TableName))
                {
                    return Expression.Property(parm, ColName.ToString());
                }
                else
                {
                    return Expression.Property(Expression.Property(parm, TableName), ColName.ToString());
                }
            }
        }
        /// <summary>
        /// 获取子表（列）类型
        /// </summary>
        /// <param name="TableType"></param>
        /// <returns></returns>
        public System.Reflection.PropertyInfo getPropertyInfo(Type TableType)
        {
            if (ColName is ChildTableAttribute)
            {
                if (string.IsNullOrEmpty(TableName))
                {
                    return (ColName as ChildTableAttribute).getPropertyInfo(TableType);
                }
                else
                {
                    return (ColName as ChildTableAttribute).getPropertyInfo(TableType.GetProperty(TableName).PropertyType);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(TableName))
                {
                    return TableType.GetProperty(ColName.ToString());
                }
                else
                {
                    return TableType.GetProperty(TableName).PropertyType.GetProperty(ColName.ToString());
                }

            }
        }
    }
}
