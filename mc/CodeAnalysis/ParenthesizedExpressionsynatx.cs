using System.Collections.Generic;

namespace Minsk_Wang
{
    public sealed class ParenthesizedExpressionsynatx: ExpressionSyntax 
    {
        public ParenthesizedExpressionsynatx(SyntaxToken openParenteisToken, ExpressionSyntax expression, SyntaxToken closeParenteisToken) 
        {
            OpenParentheisToken = openParenteisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenteisToken;
        }

        public override SynaxKind Kind => SynaxKind.ParenthesisToken; 

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return OpenParentheisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }

        public SyntaxToken OpenParentheisToken { get; }
        public ExpressionSyntax Expression {get;}
        public SyntaxToken CloseParenthesisToken { get; }
    }

}

