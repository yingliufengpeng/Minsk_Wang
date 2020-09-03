namespace Minsk_Wang
{
    internal static class SynatxFacts 
    {
        public static int GetBinaryOperatorPrecedence(this SynaxKind synaxKind)
        {
            switch (synaxKind)
            {
             
                case SynaxKind.PLusToken:
                case SynaxKind.MinusToken:
                    return 1;
                case SynaxKind.StarToken:
                case SynaxKind.SlashToken:
                    return 2;
                case SynaxKind.UnaryExpressonToken:
                    return 3;
                default:
                    return 0;

            }
        }
    }

}

