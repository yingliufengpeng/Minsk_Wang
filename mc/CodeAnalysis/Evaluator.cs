using System;

namespace Minsk_Wang
{
    internal sealed class Evaluator 
    {
        public Evaluator(BoundExpression root)
        {
            Root = root; 
        }

        BoundExpression Root {get;}

        public object Evaluate() 
        {

            return EvaluateExpression(Root);
        }

        private object EvaluateExpression(BoundExpression root)
        {
            // BinaryExpression 
            // NumberExpression 
            // Console.WriteLine($"root is {root}");

            if (root is BoundLiteralExpresion n) 
                return  n.Value;

            if (root is BoundBinaryExpression bin) 
            {
                // Console.WriteLine($"bin left is ${bin.Left}");
                // Console.WriteLine($"bin right is ${bin.Right}");
                var left = EvaluateExpression(bin.Left);
                var right = EvaluateExpression(bin.Right);
                var op = bin.Op.BoundBinaryOperatorKind;
 
                switch (op)
                {
                   
                    case BoundBinaryOperatorKind.Addition:
                        return (int)left + (int)right;
                    case BoundBinaryOperatorKind.Subtraction:
                        return (int)left - (int)right;
                    case BoundBinaryOperatorKind.Multiplcation:
                        return (int)left * (int)right;
                    case BoundBinaryOperatorKind.Division:
                        return (int)left / (int)right;
                    case BoundBinaryOperatorKind.LogicalAnd:
                        return (bool)left && (bool)right;
                    case BoundBinaryOperatorKind.LogicalOr:
                        return (bool)left || (bool)right;
                    default:
                        throw new Exception($"Unexpected binarry operator {op}");
                }
            }

            // if (root is ParenthesizedExpressionsynatx p) 
            // {
            //     return EvaluateExpression(p.Expression);
            // }

            if (root is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);
                var op = u.Op.BoundUnaryOperatorKind;
                switch(op)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return (int) operand;
                    case BoundUnaryOperatorKind.Negation:
                        return -(int) operand;
                    case BoundUnaryOperatorKind.LogicalNegation:
                        return !(bool) operand;
                    default:
                        throw new Exception($"Unexpected unary operator {op}");
                }

             }

            throw new Exception($"Unexpected node {Root.Kind}");
        }
    }

}

