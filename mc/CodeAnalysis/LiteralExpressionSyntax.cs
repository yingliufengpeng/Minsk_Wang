using System.Collections.Generic;

namespace Minsk_Wang
{
    public sealed class LiteralExpressionSyntax: ExpressionSyntax 
    {
        public LiteralExpressionSyntax(SyntaxToken numberToken) 
        {
            NumberToken = numberToken;
        }
        
        public override SynaxKind Kind => SynaxKind.LiteralExpression; 

        public SyntaxToken NumberToken {get;}

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return NumberToken;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}

