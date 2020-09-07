using System;
using System.Collections.Generic;

namespace Minsk_Wang
{
    internal sealed class Binder 
    {
        private readonly List<string> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;
        public BoundExpression BindExpression(ExpressionSyntax syntax)
        {
            switch(syntax.Kind)
            {
                case SynaxKind.LiteralExpression:
                    return BoundLiteralExpresion((LiteralExpressionSyntax)syntax);
                case SynaxKind.UnaryExpressonToken:
                    return BoundUnaryExpression((UnaryxpressionSyntax)syntax);
                case SynaxKind.BinaryExpression:
                    return BoundBinaryExpression((BinaryExpressionSyntax)syntax);
                
                default:
                    throw new NotImplementedException($"Unexpected syntax {syntax.Kind}");
            }
        }

        private BoundExpression BoundLiteralExpresion(LiteralExpressionSyntax syntax)
        {
            var value = syntax.Value;
            return new BoundLiteralExpresion(value);
        }


        private BoundExpression BoundUnaryExpression(UnaryxpressionSyntax syntax)
        {
            var boundOperand = BindExpression(syntax.Expr);
            var op = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);

            if (op == null) 
            {
                _diagnostics.Add($"Unary operator '{syntax.OperatorToken.Text}' kind '{boundOperand.Kind}' is not defined for type {boundOperand.Type}");
                return boundOperand;
            }
            return new BoundUnaryExpression(op, boundOperand);

        }

        private BoundExpression BoundBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var op = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);
            // Console.WriteLine($"kind {syntax.OperatorToken.Kind} boundLeft {boundLeft.Type} boundRight {boundRight.Type} op is {op} ");
            if (op == null) 
            {
                _diagnostics.Add($"Binary operator '{syntax.OperatorToken.Text}' is not defined for type {boundLeft.Type} {boundRight}");
                return boundLeft;
            }
            
            return new BoundBinaryExpression(boundLeft, op, boundRight);
        }
    }
 
}