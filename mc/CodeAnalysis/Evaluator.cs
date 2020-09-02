using System;

namespace Minsk_Wang
{
    class Evaluator 
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

            if (root is NumberExpressionSyntax n) 
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
                    return l_value = r_value;
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
            throw new Exception($"Unexpected node {Root.Kind}");
        }
    }

}

