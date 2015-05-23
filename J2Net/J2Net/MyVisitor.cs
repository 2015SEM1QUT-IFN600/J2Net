
//////////////////////////
//Just a test - Ignore it
//////////////////////////


using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using J2Net.Grammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2Net
{
    class MyVisitor : JavaBaseVisitor<string>
    {
        public override string VisitLiteral(JavaParser.LiteralContext context)
        {
            return Convert.ToString(int.Parse(context.IntegerLiteral().GetText()));
        }

        //public override int VisitAddSub(JavaParser context)
        //{
        //    int left = Visit(context.expr(0));
        //    int right = Visit(context.expr(1));
        //    if (context.op.Type == JavaParser.ADD)
        //    {
        //        return left + right;
        //    }
        //    else
        //    {
        //        return left - right;
        //    }
        //}
        //public override string VisitExpression(JavaParser.ExpressionContext context)
        //{
        //    string left = Visit(context.assignmentExpression().conditionalExpression().oderOfOperations1().oderOfOperations2().oderOfOperations3().oderOfOperations4().oderOfOperations5().oderOfOperations6().oderOfOperations7().oderOfOperations8().oderOfOperations9().oderOfOperations10().unaryExpression().unaryExpressionNotPlusMinus().postfixExpression().primary().primaryNoNewArray_no_primary().literal()).ToString();
        //    string right = Visit(context.assignmentExpression().conditionalExpression().oderOfOperations1().oderOfOperations2().oderOfOperations3().oderOfOperations4().oderOfOperations5().oderOfOperations6().oderOfOperations7().oderOfOperations8().oderOfOperations9().oderOfOperations10().unaryExpression().unaryExpressionNotPlusMinus().postfixExpression().primary().primaryNoNewArray_no_primary().literal()).ToString();
        //    //if (context.assignmentExpression().assignment().assignmentOperator().ADD_ASSIGN().GetChild(0).GetType() == JavaParser.ADD)
        //    //{
        //    return left + "+" + right;
        //    //}
        //    //else
        //    //{
        //    //    return left - right;
        //    //}
        //}

        //public override int VisitMulDiv(JavaParser.MulDivContext context)
        //{
        //    int left = Visit(context.expr(0));
        //    int right = Visit(context.expr(1));
        //    if (context.op.Type == JavaParser.MUL)
        //    {
        //        return left * right;
        //    }
        //    else
        //    {
        //        return left / right;
        //    }
        //}

        //public override int VisitParens(JavaParser.ParensContext context)
        //{
        //    return Visit(context.expr());
        //}
    }
}
