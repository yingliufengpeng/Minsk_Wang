using System.Collections.Generic;
using System.Linq;

namespace Minsk_Wang
{
    public sealed class SyntaxToken : SyntaxNode
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


        public override string ToString()
        {
            return $"Kind {Kind} Position {Position} Text is {Text} Value is {Value} ";
        }
    }

}

