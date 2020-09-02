using System.Collections.Generic;
using System.Linq;

namespace Minsk_Wang
{
    sealed class SytaxTree 
    {
        public SytaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken) 
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOffFileToken = endOfFileToken;
        }

        public IReadOnlyList<string> Diagnostics {get;}
        public ExpressionSyntax Root {get;}
        public SyntaxToken EndOffFileToken {get;}

        public static SytaxTree Parse(string text) 
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
        
    }

}

