using System;

namespace Minsk_Wang
{
    internal static class SynatxFacts 
    {
        public static int GetBinaryOperatorPrecedence(this SynaxKind synaxKind)
        {
            switch (synaxKind)
            {
                case SynaxKind.StarToken:
                case SynaxKind.SlashToken:
                    return 5;
                case SynaxKind.PLusToken:
                case SynaxKind.MinusToken:
                    return 4;
                case SynaxKind.UnaryExpressonToken:
                    return 3;
                case SynaxKind.AmpersandAmpersandToken:
                    return 2;
                case SynaxKind.PipePipeToken: 
                    return 1;
                default:
                    return 0;

            }
        }

        internal static SynaxKind GetKeywordKind(string text)
        {
            switch (text) 
            {
                case "true":
                    return SynaxKind.TrueKeyword;
                case "false":
                    return SynaxKind.FalseKeyword;
                default:
                    return SynaxKind.IdentiferToken;
            }
        }
    }

}

