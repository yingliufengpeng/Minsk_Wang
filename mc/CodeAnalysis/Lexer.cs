using System.Collections.Generic;

namespace Minsk_Wang
{
    public sealed class Lexer
    {
        private readonly string _text;
        private int _position;

        private List<string> _dianostics  = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }

        private void Next()
        {
            _position++;
        }

        public IEnumerable<string> Diagnostics => _dianostics;

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';

                return _text[_position];
            }
        }

        public SyntaxToken NextToken()
        {
            // <number>
            // + - * / ( )
            // <whitespace>

            if (_position >= _text.Length)
            {
                return new SyntaxToken(SynaxKind.EndOfFileToken, _position, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current))
                {
                    Next();
                }
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _dianostics.Add($"The number {_text} isn't valid  Int32");
                }


                return new SyntaxToken(SynaxKind.NumberToken, start, text, value);

            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }
                var length = _position - start;
                var text = _text.Substring(start, length);
                // int.TryParse(text, out var value);
                return new SyntaxToken(SynaxKind.WhiteSpaceToken, start, text, "");

            }

            if (Current == '+')
            {
                return new SyntaxToken(SynaxKind.PLusToken, _position++, "+", null);
            }
            else if (Current == '-')
            {
                return new SyntaxToken(SynaxKind.MinusToken, _position++, "-", null);
            }
            else if (Current == '*')
            {
                return new SyntaxToken(SynaxKind.StarToken, _position++, "*", null);
            }
            else if (Current == '/')
            {
                return new SyntaxToken(SynaxKind.SlashToken, _position++, "+", null);
            }
            else if (Current == '(')
            {
                return new SyntaxToken(SynaxKind.OpenParenthesisToken, _position++, "(", null);
            }
            else if (Current == ')')
            {
                return new SyntaxToken(SynaxKind.CloseParenthesisToken, _position++, ")", null);
            }

            _dianostics.Add($"ERROR: bad character input: {Current}");
            return new SyntaxToken(SynaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);


        }


    }

}

