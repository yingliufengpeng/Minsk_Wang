using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minsk_Wang2
{
    // 1 + 2 + 3 
    // 
    class Program
    {
        static void Main2(string[] args)
        {
             bool showTree = false;

            while (true)
            {
                var line = Console.ReadLine();
                Console.WriteLine(line);

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Show parse trees." : "Not showing parse trees");
                    continue;
                } 
                else if (line == "#cls") 
                {
                    Console.Clear();
                    continue;
                }
                
                // var lexer = new Lexer(line);
                var syntaxTree = SytaxTree.Parse(line); 

                var color = Console.ForegroundColor;

                if (showTree)
                {
                   
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root, "", true);
        
                    Console.ForegroundColor = color;
                }

                

                if (syntaxTree.Diagnostics.Any()) 
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }        
                    Console.ForegroundColor = color;
                } 
                else 
                {
                    var evaluator = new Evaluator(syntaxTree.Root);

                    var result = evaluator.Evaluate();

                    Console.WriteLine($"the result is {result}");
                }
 
            }

        }
    
        static void PrettyPrint(SyntaxNode node, string intent, bool is_last_node)     
        {
            var maker = is_last_node ? "|___" : ">---";
            // Console.Write(maker); 
            Console.Write(intent);
            Console.Write(maker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {   
                Console.Write(" ");
                Console.Write(t.Value);
            }

            intent = intent + (is_last_node ? "    " : "|   ");
            Console.WriteLine();
            foreach(var n in node.GetChildren()) 
            {
                is_last_node = n == node.GetChildren().Last();
                PrettyPrint(n, intent, is_last_node);
            }
        }
    }

    enum SynaxKind
    {
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
        BinaryExpression,
        NumberExpression,
        ParenthesisToken,
    }

    class SyntaxToken: SyntaxNode
    {
        public SyntaxToken(SynaxKind kind, int positon, string text, object value)
        {
            Kind = kind;
            Position = positon;
            Text = text;
            Value = value;

        }

        public override IEnumerable<SyntaxNode> GetChildren() => Enumerable.Empty<SyntaxNode>();

        public override SynaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }

        public object Value { get; }

    }

    class Lexer
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

    abstract class SyntaxNode 
    {
        public abstract SynaxKind Kind {get;}
        public abstract IEnumerable<SyntaxNode> GetChildren();
    } 

    abstract class ExpressionSyntax: SyntaxNode 
    {
        
    }

    sealed class ParenthesizedExpressionsynatx: ExpressionSyntax 
    {
        public ParenthesizedExpressionsynatx(SyntaxToken openParenteisToken, ExpressionSyntax expression, SyntaxToken closeParenteisToken) 
        {
            OpenParentheisToken = openParenteisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenteisToken;
        }

        public override SynaxKind Kind => SynaxKind.ParenthesisToken; 

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return OpenParentheisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }

        public SyntaxToken OpenParentheisToken { get; }
        public ExpressionSyntax Expression {get;}
        public SyntaxToken CloseParenthesisToken { get; }
    }

    sealed class NumberExpressionSyntax: ExpressionSyntax 
    {
        public NumberExpressionSyntax(SyntaxToken numberToken) 
        {
            NumberToken = numberToken;
        }
        
        public override SynaxKind Kind => SynaxKind.NumberExpression; 

        public SyntaxToken NumberToken {get;}

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return NumberToken;
        }

    }

    sealed class BinaryExpressionSyntax: ExpressionSyntax 
    {
        
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
          Left = left;
          OperatorToken = operatorToken;
          Right = right;
        }
        public override SynaxKind Kind => SynaxKind.BinaryExpression;

        public ExpressionSyntax Left {get;}
        public SyntaxToken OperatorToken {get;}
        public ExpressionSyntax Right {get;}

        public override IEnumerable<SyntaxNode> GetChildren() 
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }

    sealed class SytaxTree 
    {
        public SytaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken) 
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOffFileToken = endOfFileToken;
        }

        public IReadOnlyList<string> Diagnostics {get;}
        public ExpressionSyntax Root {get;}
        public SyntaxToken EndOffFileToken {get;}

        public static SytaxTree Parse(string text) 
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
        
    }

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

    class Evaluator 
    {
        public Evaluator(ExpressionSyntax root)
        {
            Root = root; 
        }

        ExpressionSyntax Root {get;}

        public int Evaluate() 
        {

            return EvaluateExpression(Root);
        }

        private int EvaluateExpression(ExpressionSyntax root)
        {
            // BinaryExpression 
            // NumberExpression 
            // Console.WriteLine($"root is {root}");

            if (root is NumberExpressionSyntax n) 
                return (int) n.NumberToken.Value;

            if (root is BinaryExpressionSyntax bin) 
            {
                // Console.WriteLine($"bin left is ${bin.Left}");
                // Console.WriteLine($"bin right is ${bin.Right}");
                var l_value = EvaluateExpression(bin.Left);
                var r_value = EvaluateExpression(bin.Right);
                var op = bin.OperatorToken.Kind;
                if (op == SynaxKind.PLusToken) 
                {
                    return l_value + r_value;
                }
                if (op == SynaxKind.MinusToken) 
                {
                    return l_value = r_value;
                }
                if (op == SynaxKind.StarToken) 
                {
                    return l_value * r_value;
                }
                if (op == SynaxKind.SlashToken) 
                {
                    return l_value / r_value;
                }
                throw new Exception($"Unexpected binarry operator {bin.OperatorToken.Kind}");
            }

            if (root is ParenthesizedExpressionsynatx p) 
            {
                return EvaluateExpression(p.Expression);
            }
            throw new Exception($"Unexpected node {Root.Kind}");
        }
    }

}

