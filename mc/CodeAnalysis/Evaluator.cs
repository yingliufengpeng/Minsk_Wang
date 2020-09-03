using System;

namespace Minsk_Wang
{
    public sealed class Evaluator 
    {
        public Evaluator(ExpressionSyntax root)
        {
            Root = root; 
        }

        ExpressionSyntax Root {get;}

        public int Evaluate() 
        {

            return EvaluateExpression(Root);
        }

        private int EvaluateExpression(ExpressionSyntax root)
        {
            // BinaryExpression 
            // NumberExpression 
            // Console.WriteLine($"root is {root}");

            if (root is LiteralExpressionSyntax n) 
                return (int) n.NumberToken.Value;

            if (root is BinaryExpressionSyntax bin) 
            {
                // Console.WriteLine($"bin left is ${bin.Left}");
                // Console.WriteLine($"bin right is ${bin.Right}");
                var l_value = EvaluateExpression(bin.Left);
                var r_value = EvaluateExpression(bin.Right);
                var op = bin.OperatorToken.Kind;
                if (op == SynaxKind.PLusToken) 
                {
                    return l_value + r_value;
                }
                if (op == SynaxKind.MinusToken) 
                {
                    return l_value - r_value;
                }
                if (op == SynaxKind.StarToken) 
                {
                    return l_value * r_value;
                }
                if (op == SynaxKind.SlashToken) 
                {
                    return l_value / r_value;
                }
                throw new Exception($"Unexpected binarry operator {bin.OperatorToken.Kind}");
            }

            if (root is ParenthesizedExpressionsynatx p) 
            {
                return EvaluateExpression(p.Expression);
            }

            if (root is UnaryxpressionSyntax u)
            {
                var i = 0;
                switch(u.OperatorToken.Kind)
                {
                    case SynaxKind.PLusToken:
                        i = 1;
                        break;
                    case SynaxKind.MinusToken:
                        i = -1;
                        break; 
                    default:
                        new Exception($"Unexpected unary operator {u.OperatorToken.Kind}");
                        break;
                }

                // Console.WriteLine($"i is {i}");

                return i * EvaluateExpression(u.Expr);
            }

            throw new Exception($"Unexpected node {Root.Kind}");
        }
    }

}

