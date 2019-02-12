using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.AttributeHelper
{
    public class SearchAttribute : Attribute
    {
        public SearchAttribute(string BaseName = null, Symbol Symbol = Symbol.等于, string TableName = null)
        {
            this.BaseName = BaseName;
            this.Symbol = Symbol;
            this.TableName = TableName;
        }

        public string TableName;

        public string BaseName;

        public Symbol Symbol;

    }

    public enum Symbol
    {
        等于,
        大于,
        小于,
        大于等于,
        小于等于,
        相似,
        包含,
        不等于,
        无效,
        空
    }
}
