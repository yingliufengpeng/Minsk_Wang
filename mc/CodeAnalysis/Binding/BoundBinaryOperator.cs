using System;

namespace Minsk_Wang
{
    internal sealed class BoundBinaryOperator 
    {

        private BoundBinaryOperator(SynaxKind synaxKind, BoundBinaryOperatorKind kind, Type operandType) 
            : this(synaxKind, kind, operandType, operandType, operandType)
        {

        }
        
        private BoundBinaryOperator(SynaxKind synaxKind, BoundBinaryOperatorKind boundBinaryOperatorKind, Type leftType, Type rightType, Type resultType) 
        {
            SynaxKind = synaxKind;
            BoundBinaryOperatorKind = boundBinaryOperatorKind;
            LeftType = leftType;
            RightType = rightType;
            ResultType = resultType;
        }

        public SynaxKind SynaxKind { get; }
        public BoundBinaryOperatorKind BoundBinaryOperatorKind { get; }
        public Type LeftType { get; }
        public Type RightType { get; }
        public Type ResultType { get; }

        private static BoundBinaryOperator[] _operators = 
        {
            new BoundBinaryOperator(SynaxKind.PLusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
            new BoundBinaryOperator(SynaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
            new BoundBinaryOperator(SynaxKind.StarToken, BoundBinaryOperatorKind.Multiplcation, typeof(int)),
            new BoundBinaryOperator(SynaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),

            new BoundBinaryOperator(SynaxKind.AmpersandAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
            new BoundBinaryOperator(SynaxKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
        };

        public static BoundBinaryOperator Bind(SynaxKind synaxKind, Type leftType, Type rightType) {
            foreach(var op in _operators)
            {
                if (op.SynaxKind == synaxKind  && op.LeftType == leftType && op.RightType == rightType)
                    return op;
            }

            return null;
        }

        
    }

  

}