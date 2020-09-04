using System;

namespace Minsk_Wang
{
    internal sealed class BoundBinaryExpression: BoundExpression 
    {

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind operatorKind, BoundExpression right) 
        {
            Left = left;
            OperatorKind = operatorKind;
            Right = right;
        }

        public override BoundeNodeKind Kind => BoundeNodeKind.BinaryExpression;
        public override Type Type  => Left.Type;

        public BoundExpression Left { get; }
        public BoundBinaryOperatorKind OperatorKind { get; }
        public BoundExpression Right { get; }
    }

  

}