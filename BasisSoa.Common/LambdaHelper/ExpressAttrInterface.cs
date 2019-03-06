using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BasisSoa.Common.LambdaHelper
{
    public interface ExpressAttrInterface
    {
        Expression GetExpression(Expression parm, Type TableType);
    }



    public class ExpressAttrCommon
    {
        public static object getExpressAttr(object InOb)
        {

            if (InOb is string)
            {
                return InOb;
            }
            else
            {
                if (InOb.GetType().IsArray)
                {
                    int Length = InOb.GetType().GetArrayRank();
                    object[] List = (object[])InOb;
                    // 子表
                    if (Length == 2)
                    {

                        string TableName = List[0].ToString();
                        object ColName = List[1];
                        return new ChildTableAttribute(TableName, getExpressAttr(ColName));
                    }
                    else if (Length == 3 && List[2] is Operation)
                    {
                        object Left = getExpressAttr(List[0]);
                        object Right = getExpressAttr(List[1]);
                        Operation op = (Operation)List[2];
                        return new OperationAttribute(Left, Right, op);
                    }
                }
            }
            return InOb;
        }

    }
}
