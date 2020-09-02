using System.Collections.Generic;

namespace Minsk_Wang
{
    sealed class NumberExpressionSyntax: ExpressionSyntax 
    {
        public NumberExpressionSyntax(SyntaxToken numberToken) 
        {
            NumberToken = numberToken;
        }
        
        public override SynaxKind Kind => SynaxKind.NumberExpression; 

        public SyntaxToken NumberToken {get;}

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return NumberToken;
        }

    }

}

