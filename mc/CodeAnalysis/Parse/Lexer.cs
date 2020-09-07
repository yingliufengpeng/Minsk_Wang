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

        private char Peek(int offset) 
        {
            var index = _position + offset;
            if (index >= _text.Length) 
                return '\0';

            return _text[index];
        
            
        }

        public IEnumerable<string> Diagnostics => _dianostics;

        private char Current => Peek(0);

        private char Lookahead => Peek(1);

        public SyntaxToken Lex()
        {

            if (_position >= _text.Length) // Append EndOfFileToken to Parser
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

            if (char.IsLetter(Current))
            {
                var start = _position;
                while (char.IsLetter(Current))
                    Next(); 
                
                var length = _position - start; 
                var text = _text.Substring(start, length);
                var kind = SynatxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);

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

            switch (Current)
            {
                case '+':
                    return new SyntaxToken(SynaxKind.PLusToken, _position++, "+", null);
                case '-':
                    return new SyntaxToken(SynaxKind.MinusToken, _position++, "-", null);
                case '*':
                    return new SyntaxToken(SynaxKind.StarToken, _position++, "*", null);
                case '/':
                    return new SyntaxToken(SynaxKind.SlashToken, _position++, "+", null);
                case '(':
                    return new SyntaxToken(SynaxKind.OpenParenthesisToken, _position++, "(", null);
                case ')':
                    return new SyntaxToken(SynaxKind.CloseParenthesisToken, _position++, ")", null);
                case '&':
                    if (Lookahead == '&')
                        return new SyntaxToken(SynaxKind.AmpersandAmpersandToken, _position += 2, "&&", null);
                    break;    
                case '|':
                    if (Lookahead == '|')
                        return new SyntaxToken(SynaxKind.PipePipeToken, _position += 2, "||", null);
                    break;  

                case '=':
                    if (Lookahead == '=') 
                    {
                        return new SyntaxToken(SynaxKind.EqualsEqualsToken, _position += 2, "==", null);
                    }
                    break;

                case '!':
                    if (Lookahead == '=') 
                        return new SyntaxToken(SynaxKind.BangEqualsToken, _position += 2, "!=", null);
                    else
                        return new SyntaxToken(SynaxKind.BangToken, _position++, "!", null);

            }

            _dianostics.Add($"ERROR: bad character input: {Current}");
            return new SyntaxToken(SynaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);


        }


    }

}

