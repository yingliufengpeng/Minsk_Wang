using System.Collections.Generic;

namespace Minsk_Wang
{
    public abstract class SyntaxNode 
    {
        public abstract SynaxKind Kind {get;}
        public abstract IEnumerable<SyntaxNode> GetChildren();
    } 

}

