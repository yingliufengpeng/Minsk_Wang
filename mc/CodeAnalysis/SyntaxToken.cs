using System.Collections.Generic;
using System.Linq;

namespace Minsk_Wang
{
    class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SynaxKind kind, int positon, string text, object value)
        {
            Kind = kind;
            Position = positon;
            Text = text;
            Value = value;

        }

        public override IEnumerable<SyntaxNode> GetChildren() => Enumerable.Empty<SyntaxNode>();

        public override SynaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public object Value { get; }

    }

}

