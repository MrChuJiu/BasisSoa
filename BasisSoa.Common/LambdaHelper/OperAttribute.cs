using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.LambdaHelper
{

    public class OperAttribute : Attribute
    {
        public OperSymol Symol;
        /// <summary>
        /// （外键，子分表）绑定
        /// </summary>
        /// <param name="TableName">子分表名</param>
        /// <param name="ColName">列名</param>
        public OperAttribute(OperSymol Symol)
        {
            this.Symol = Symol;
        }
        public string ToMethodStr()
        {
            switch (this.Symol)
            {
                case OperSymol.Sum:
                    return "Sum";
                case OperSymol.Max:
                    return "Max";
                case OperSymol.Min:
                    return "Min";
                case OperSymol.Avg:
                    return "Average";
                case OperSymol.Count:
                    return "Count";
            }
            return "";
        }
        public string ToMethodStrFul(Type type)
        {
            string MeStr = ToMethodStr();
            return MeStr + "<" + type.Name + ">";
        }
    }
    public enum OperSymol
    {
        Sum,
        Max,
        Min,
        Avg,
        Count,


    }
}
