using System;
using System.Collections.Generic;

namespace Minsk_Wang
{
    //
    // +1 
    // -1 * -3
    // -(3 + 3)
    //
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        
        private List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => _diagnostics;

        
        public SytaxTree Parse() 
        {
           var express = ParseExpression();
           var endOfFile = MatchToken(SynaxKind.EndOfFileToken);

           return new SytaxTree(Diagnostics, express, endOfFile);

        }

        private ExpressionSyntax ParseExpression(int parentPrecedence=0)
        {
            var left = ParserPrimaryExpression();
            while (true) 
            {
                // var precedence = SynatxFacts.GetBinaryOperatorPrecedence(Current.Kind);
                var precedence = Current.Kind.GetBinaryOperatorPrecedence();;

                if(precedence == 0 || precedence <= parentPrecedence)
                    break; 
                
                // Console.WriteLine("continue....");

                 var opartorToken = NextToken(); 
                //  Console.WriteLine($"operatorToken {opartorToken}");
                 
                 var right = ParseExpression(precedence);

                 left = new BinaryExpressionSyntax(left, opartorToken, right);
            }

            return left;
        }


        public Parser(string text)  // generate token stream
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token; 
            do {
                token = lexer.Lex(); 

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
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SynaxKind kind) 
        {
            // Console.WriteLine($"number kind is {kind}");
            if (Current.Kind == kind) 
                return NextToken();

            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}> expected: {kind}");
            
            return new SyntaxToken(kind, Current.Position, null, null);
        }


        // private ExpressionSyntax ParseExpression2() 
        // {
        //     return ParseTerm();
        // }

        // private ExpressionSyntax ParseTerm() 
        // {
        //     var left = ParseFacotr(); 

        //     while (Current.Kind == SynaxKind.PLusToken ||
        //            Current.Kind == SynaxKind.MinusToken 
        //         )
        //     {
        //         var operaotrToken = NextToken(); 
        //         var right = ParseFacotr();

        //         left = new BinaryExpressionSyntax(left, operaotrToken, right);

        //     }

        //     return left;

        // }

        // private ExpressionSyntax ParseFacotr()
        // {
        //     var left = ParserPrimaryExpression(); 

        //     while (
        //            Current.Kind == SynaxKind.StarToken || 
        //            Current.Kind == SynaxKind.SlashToken
        //         )
        //     {
        //         var operaotrToken = NextToken(); 
        //         var right = ParserPrimaryExpression();

        //         left = new BinaryExpressionSyntax(left, operaotrToken, right);

        //     }

        //     return left;
        // }

        private ExpressionSyntax ParserPrimaryExpression()
        {
            switch (Current.Kind)
            {
                case SynaxKind.OpenParenthesisToken:
                    {
                        var left = NextToken();
                        var expression = ParseExpression();
                        var right = MatchToken(SynaxKind.CloseParenthesisToken);

                        return new ParenthesizedExpressionsynatx(left, expression, right);
                    }

                case SynaxKind.PLusToken:
                case SynaxKind.MinusToken:
                    {
                        var opertator = NextToken();
                        var expression = ParserPrimaryExpression();
                        return new UnaryxpressionSyntax(opertator, expression);
                    }
                
                case SynaxKind.TrueKeyword:
                case SynaxKind.FalseKeyword:
                    {
                        var value = Current.Kind == SynaxKind.TrueKeyword ? true : false;
                        var token = NextToken();
                        return new LiteralExpressionSyntax(token, value);
                        // throw new System.Exception("fsdfs");
                    }

                case SynaxKind.BangToken:
                {
                    var opertator = NextToken();
                    var expression = ParserPrimaryExpression();
                    return new UnaryxpressionSyntax(opertator, expression);
                }
            }

            var numberToken = MatchToken(SynaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);

        }
    }

}

