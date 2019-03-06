using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BasisSoa.Common.LambdaHelper
{
    /// <summary>
    /// 外表标签
    /// </summary>
    public class DataAttribute : Attribute, ExpressAttrInterface
    {
        public string TableName;
        public string ColName;
        /// <summary>
        /// （外键，子分表）绑定
        /// </summary>
        /// <param name="TableName">子分表名</param>
        /// <param name="ColName">列名</param>
        public DataAttribute(string TableName, string ColName)
        {
            this.TableName = TableName;
            this.ColName = ColName;
        }
        public Expression GetExpression(Expression parm, Type TableType)
        {
            if (string.IsNullOrEmpty(TableName))
            {
                return Expression.Property(parm, ColName);
            }
            else
            {
                return Expression.Property(Expression.Property(parm, TableType.GetProperty(TableName)), ColName);
            }

        }
    }
}
