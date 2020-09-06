using System;

namespace Minsk_Wang
{
    internal sealed class BoundUnaryOperator 
    {

        private BoundUnaryOperator(SynaxKind synaxKind, BoundUnaryOperatorKind kind, Type operandType) 
            : this(synaxKind, kind, operandType, operandType)
        {

        }
        private BoundUnaryOperator(SynaxKind synaxKind, BoundUnaryOperatorKind boundUnaryOperatorKind, Type operandType, Type resultType) 
        {
            SynaxKind = synaxKind;
            BoundUnaryOperatorKind = boundUnaryOperatorKind;
            OperandType = operandType;
            ResultType = resultType;
        }

        public SynaxKind SynaxKind { get; }
        public BoundUnaryOperatorKind BoundUnaryOperatorKind { get; }
        public Type OperandType { get; }
        public Type ResultType { get; }

        private static BoundUnaryOperator[] _operators = 
        {
            new BoundUnaryOperator(SynaxKind.PLusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
            new BoundUnaryOperator(SynaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int)),
            new BoundUnaryOperator(SynaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, typeof(int)),
        };

        public static BoundUnaryOperator Bind(SynaxKind synaxKind, Type operandType) {
            foreach(var op in _operators)
            {
                if (op.SynaxKind == synaxKind  && op.OperandType == operandType)
                    return op;
            }

            return null;
        }

        
    }

  

}