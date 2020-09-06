using System.Collections.Generic;

namespace Minsk_Wang
{
    public sealed class LiteralExpressionSyntax: ExpressionSyntax 
    {

        public LiteralExpressionSyntax(SyntaxToken literalToken)
            : this(literalToken, literalToken.Value) 
        {
            
        }

        public LiteralExpressionSyntax(SyntaxToken literalToken, object value) 
        {
            LiteralToken = literalToken;
            Value = value;
        }
        
        public override SynaxKind Kind => SynaxKind.LiteralExpression; 
        public SyntaxToken LiteralToken { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return LiteralToken;
        }

       
    }

}

