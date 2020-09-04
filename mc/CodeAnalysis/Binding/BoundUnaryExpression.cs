using System;

namespace Minsk_Wang
{
    internal sealed class BoundUnaryExpression: BoundExpression 
    {

        public BoundUnaryExpression(BoundUnaryOperatorKind operatorKind, BoundExpression operand) 
        {
            OperatorKind = operatorKind;
            Operand = operand;
        }

        public override BoundeNodeKind Kind => BoundeNodeKind.UnaryExpression;
        public override Type Type  => Operand.Type;
        public BoundUnaryOperatorKind OperatorKind { get;}
        public BoundExpression Operand { get; }
    }

  

}