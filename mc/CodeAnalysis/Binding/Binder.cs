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

        private BoundExpression BoundBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperatorKind = BindBinaryOperatorKind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);
            if (boundOperatorKind == null) 
            {
                _diagnostics.Add($"Binary operator '{syntax.OperatorToken.Text}' is not defined for type {boundLeft.Type} {boundRight}");
                return boundLeft;
            }
            
            return new BoundBinaryExpression(boundLeft, boundOperatorKind.Value, boundRight);
        }

     

        private BoundExpression BoundUnaryExpression(UnaryxpressionSyntax syntax)
        {
            var boundOperand = BindExpression(syntax.Expr);
            var boundOperatorKind = BindUnaryOperatorKind(syntax.OperatorToken.Kind, boundOperand.Type);

            if (boundOperatorKind == null) 
            {
                _diagnostics.Add($"Unary operator '{syntax.OperatorToken.Text}' is not defined for type {boundOperand.Type}");
                return boundOperand;
            }
                

            return new BoundUnaryExpression(boundOperatorKind.Value, boundOperand);

        }

     

        private BoundExpression BoundLiteralExpresion(LiteralExpressionSyntax syntax)
        {
            var value = syntax.Value;
            return new BoundLiteralExpresion(value);
        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(SynaxKind kind, Type leftType, Type rightType)
        {
           

            if (leftType == typeof(int) && rightType == typeof(int)) 
            {
                switch(kind) 
                {
                    case SynaxKind.PLusToken:
                        return BoundBinaryOperatorKind.Addition;
                    case SynaxKind.MinusToken:
                        return BoundBinaryOperatorKind.Subtraction;
                    case SynaxKind.StarToken:
                        return BoundBinaryOperatorKind.Multiplcation;
                    case SynaxKind.SlashToken:
                        return BoundBinaryOperatorKind.Division;
                    default:
                        throw new Exception($"Unexcept binary operator {kind}");
                }
            }


            if (leftType == typeof(bool) && rightType == typeof(bool)) 
            {
                switch(kind) 
                {
                    case SynaxKind.AmpersandAmpersandToken:
                        return BoundBinaryOperatorKind.LogicalAnd;
                    case SynaxKind.PipePipeToken:
                        return BoundBinaryOperatorKind.LogicalOr;
                    default:
                        throw new Exception($"Unexcept binary operator {kind}");
                }
            }

            return null; 

            
        }

        private BoundUnaryOperatorKind? BindUnaryOperatorKind(SynaxKind kind, Type operandType)
        { 
            

            if (operandType == typeof(int)) 
            {
                switch(kind) 
                {
                    case SynaxKind.PLusToken:
                        return BoundUnaryOperatorKind.Identity;
                    case SynaxKind.MinusToken:
                        return BoundUnaryOperatorKind.Negation;
                    default:
                        throw new Exception($"Unexcept unary operator {kind}");
                }
            }

            if (operandType == typeof(bool)) 
            {
                switch(kind) 
                {
                    case SynaxKind.BangToken:
                        return BoundUnaryOperatorKind.LogicalNegation;
                    default:
                        throw new Exception($"Unexcept unary operator {kind}");
                }
            }

            return null; 
        }
    }

  

}