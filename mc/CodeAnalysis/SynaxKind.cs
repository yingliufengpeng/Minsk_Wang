namespace Minsk_Wang
{
    public enum SynaxKind
    {
        // Tokens
        NumberToken,
        WhiteSpaceToken,
        CloseParenthesisToken,
        StarToken,
        PLusToken,
        MinusToken,
        SlashToken,
        OpenParenthesisToken,
        BadToken,
        EndOfFileToken,

        // Expression 
        BinaryExpression,
        NumberExpression,
        ParenthesisToken,
        LiteralExpression,
        UnaryExpressonToken,
    }

}

