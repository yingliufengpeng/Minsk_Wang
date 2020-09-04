using System;

namespace Minsk_Wang
{
    internal sealed class BoundLiteralExpresion : BoundExpression
    {
 
        public BoundLiteralExpresion(object value)
        {
            Value = value;
        }
        public override BoundeNodeKind Kind => BoundeNodeKind.LiteralExpression;

        public override Type Type => Value.GetType();

        public object Value { get; }
    }

  

}