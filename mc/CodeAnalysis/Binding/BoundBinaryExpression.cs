using System;

namespace Minsk_Wang
{
    internal sealed class BoundBinaryExpression: BoundExpression 
    {

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right) 
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public override BoundeNodeKind Kind => BoundeNodeKind.BinaryExpression;
        public override Type Type  => Op.ResultType;

        public BoundExpression Left { get; }
        public BoundBinaryOperator Op { get; }
        public BoundExpression Right { get; }
    }

  

}