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

        public int Evaluate() 
        {

            return EvaluateExpression(Root);
        }

        private int EvaluateExpression(BoundExpression root)
        {
            // BinaryExpression 
            // NumberExpression 
            // Console.WriteLine($"root is {root}");

            if (root is BoundLiteralExpresion n) 
                return (int) n.Value;

            if (root is BoundBinaryExpression bin) 
            {
                // Console.WriteLine($"bin left is ${bin.Left}");
                // Console.WriteLine($"bin right is ${bin.Right}");
                var l_value = EvaluateExpression(bin.Left);
                var r_value = EvaluateExpression(bin.Right);
                var op = bin.OperatorKind;
                switch (op)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return l_value + r_value;
                    case BoundBinaryOperatorKind.Subtraction:
                        return l_value - r_value;
                    case BoundBinaryOperatorKind.Multiplcation:
                        return l_value * r_value;
                    case BoundBinaryOperatorKind.Division:
                        return l_value / r_value;
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
                switch(u.OperatorKind)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return EvaluateExpression(u.Operand);
                    case BoundUnaryOperatorKind.Netation:
                        return -EvaluateExpression(u.Operand);
                    default:
                        throw new Exception($"Unexpected unary operator {u.OperatorKind}");
                }

             }

            throw new Exception($"Unexpected node {Root.Kind}");
        }
    }

}

