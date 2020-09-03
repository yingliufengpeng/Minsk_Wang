using System.Collections.Generic;

namespace Minsk_Wang
{
    public sealed class UnaryxpressionSyntax: ExpressionSyntax 
    {
        
        public UnaryxpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax expr)
        {
          Expr = expr;
          OperatorToken = operatorToken;
        }
        public override SynaxKind Kind => SynaxKind.UnaryExpressonToken;

        public ExpressionSyntax Expr {get;}
        public SyntaxToken OperatorToken {get;}
 
        public override IEnumerable<SyntaxNode> GetChildren() 
        {
            yield return Expr;
            yield return OperatorToken;
        }
    }

}

