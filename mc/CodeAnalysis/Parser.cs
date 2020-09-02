using System.Collections.Generic;

namespace Minsk_Wang
{
    class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        
        private List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => _diagnostics;

        public Parser(string text) 
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token; 
            do {
                token = lexer.NextToken(); 

                if (token.Kind != SynaxKind.BadToken && token.Kind != SynaxKind.WhiteSpaceToken)
                {
                    tokens.Add(token);
                }
                
            } while (token.Kind != SynaxKind.EndOfFileToken);

            _diagnostics.AddRange(lexer.Diagnostics);
            _tokens = tokens.ToArray();
        }

        private SyntaxToken Peek(int offset) 
        {
            var index = _position + offset;

            if (index >= _tokens.Length) 
                index = _tokens.Length - 1;

            return _tokens[index];

        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken() 
        {
            var currnet = Current;
            _position++;
            return currnet;
        }

        private SyntaxToken Match(SynaxKind kind) 
        {
            // Console.WriteLine($"number kind is {kind}");
            if (Current.Kind == kind) 
                return NextToken();

            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}> expected: {kind}");
            
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public SytaxTree Parse() 
        {
           var express = ParseExpression();
           var endOfFile = Match(SynaxKind.EndOfFileToken);

           return new SytaxTree(Diagnostics, express, endOfFile);

        }

        private ExpressionSyntax ParseExpression() 
        {
            return ParseTerm();
        }

        private ExpressionSyntax ParseTerm() 
        {
            var left = ParseFacotr(); 

            while (Current.Kind == SynaxKind.PLusToken ||
                   Current.Kind == SynaxKind.MinusToken 
                )
            {
                var operaotrToken = NextToken(); 
                var right = ParseFacotr();

                left = new BinaryExpressionSyntax(left, operaotrToken, right);

            }

            return left;

        }

        private ExpressionSyntax ParseFacotr()
        {
            var left = ParserPrimaryExpression(); 

            while (
                   Current.Kind == SynaxKind.StarToken || 
                   Current.Kind == SynaxKind.SlashToken
                )
            {
                var operaotrToken = NextToken(); 
                var right = ParserPrimaryExpression();

                left = new BinaryExpressionSyntax(left, operaotrToken, right);

            }

            return left;
        }

        private ExpressionSyntax ParserPrimaryExpression()
        {
            if (Current.Kind == SynaxKind.OpenParenthesisToken)
            {
                var left = NextToken(); 
                var expression = ParseExpression(); 
                var right = Match(SynaxKind.CloseParenthesisToken); 

                return new ParenthesizedExpressionsynatx(left, expression, right);
            }

            var numberToken = Match(SynaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);

        }
    }

}

