using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.LambdaHelper
{
    /// <summary>
    /// 其他条件限制
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OtherKeyAttribute : Attribute
    {
        /// <summary>
        /// 验证条件
        /// </summary>
        /// <param name="CkColName">列名</param>
        /// <param name="symbol">条件</param>
        /// <param name="values">值</param>
        public OtherKeyAttribute(string CkColName, Symbol symbol, object values)
        {
            this.Symbol = symbol;
            this.CkColName = CkColName;
            this.values = values;
        }
        public Symbol Symbol { get; set; }
        public string CkColName { get; set; }
        public object values { get; set; }
    }
}
