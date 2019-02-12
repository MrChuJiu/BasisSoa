using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BasisSoa.Common.AttributeHelper
{
    /// <summary>
    /// 运算标签
    /// </summary>
    public class OperationAttribute : Attribute, ExpressAttrInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Left">左侧运算</param>
        /// <param name="Right">右侧运算</param>
        /// <param name="op">运算规则</param>
        public OperationAttribute(object Left, object Right, Operation op)
        {

            this.Left = ExpressAttrCommon.getExpressAttr(Left);
            this.Right = ExpressAttrCommon.getExpressAttr(Right);
            this.op = op;
        }
        public object Left { get; set; }
        public object Right { get; set; }

        public Operation op { get; set; }
        public Expression GetExpression(Expression parm, Type TableType)
        {
            Expression Leftex = null;
            if (Left is ExpressAttrInterface)
            {
                ExpressAttrInterface AInterface = Left as ExpressAttrInterface;
                Leftex = AInterface.GetExpression(parm, TableType);
            }
            else if (Left is string)
            {
                Leftex = Expression.Property(parm, TableType.GetProperty(Left.ToString()));
            }
            else
            {
                if (Left == null || BaseToolClass.GetObType(TableType.GetProperty(Left.ToString()).PropertyType) == BaseToolClass.GetObType(Left.GetType()))
                {
                    Leftex = Expression.Constant(Left, TableType.GetProperty(Left.ToString()).PropertyType);
                }
                else
                {
                    Leftex = Expression.Constant(Left, Left.GetType());
                }
            }
            Expression Rightex = null;
            //object RightAttr = Right.GetType().GetCustomAttributes(typeof(ExpressAttrInterface), true).FirstOrDefault();
            if (Right is ExpressAttrInterface)
            {
                ExpressAttrInterface AInterface = Right as ExpressAttrInterface;
                Rightex = AInterface.GetExpression(parm, TableType);
            }
            else if (Right is string)
            {
                Rightex = Expression.Property(parm, TableType.GetProperty(Right.ToString()));
            }
            else
            {
                if (Left == null || BaseToolClass.GetObType(TableType.GetProperty(Right.ToString()).PropertyType) == BaseToolClass.GetObType(Right.GetType()))
                {
                    Rightex = Expression.Constant(Right, TableType.GetProperty(Right.ToString()).PropertyType);
                }
                else
                {
                    Rightex = Expression.Constant(Right, Right.GetType());
                }
            }
            switch (op)
            {
                case Operation.加:
                    return Expression.Add(Leftex, Rightex);
                case Operation.减:
                    return Expression.Subtract(Leftex, Rightex);
                case Operation.乘:
                    return Expression.Multiply(Leftex, Rightex);
                case Operation.除:
                    return Expression.Divide(Leftex, Rightex);
                case Operation.余:
                    return Expression.Modulo(Leftex, Rightex);
                case Operation.反:
                    return Expression.Negate(Leftex);
                default:
                    return null;
            }
        }

    }


    public enum Operation
    {
        加 = 0,//加
        减 = 1,// 减
        乘 = 2,// 乘法
        除 = 3,// 除法
        余 = 4,//余数
        反 = 5// 反运算
    }
}
