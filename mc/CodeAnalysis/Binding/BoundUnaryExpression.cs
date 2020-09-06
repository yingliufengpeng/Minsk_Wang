using System;

namespace Minsk_Wang
{
    internal sealed class BoundUnaryExpression: BoundExpression 
    {

        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand) 
        {
            Op = op;
            Operand = operand;
        }

        public override BoundeNodeKind Kind => BoundeNodeKind.UnaryExpression;
        public override Type Type  => Operand.Type;
        public BoundUnaryOperator Op { get;}
        public BoundExpression Operand { get; }
    }

  

}