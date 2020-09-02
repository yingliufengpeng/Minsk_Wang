using System;
using System.Linq;
using System.Text;

namespace Minsk_Wang
{
    // 1 + 2 + 3 
    // 
    internal static class Program
    {
        private static void Main()
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

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root, "", true);
                    Console.ResetColor();
                }

                

                if (syntaxTree.Diagnostics.Any()) 
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }        
                    Console.ResetColor();
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

}

