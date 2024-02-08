using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Expression
    {
        private Token[]? Postfix { get; set; }
        public string StringExpression { get; init; }

        public Expression(string expression) =>
            StringExpression = expression;
        public double CalculateAt(Dictionary<char, double> variables, out Node<Token> node)
        {
            if (Postfix == null)
            {
                try
                {
                    var tokens = Token.Tokenize(StringExpression);
                    Postfix = Polish.ToInversePolishView(tokens);
                }
                catch
                {
                    node = null;
                    return double.NaN;
                }
            }

            var resultArr = Transform(variables);
            var res = CalculateTree(resultArr, out var resNode);

            resultArr = null;

            node = resNode;
            return res;
        }
        private static double CalculateTree(Token[] tokens, out Node<Token> tree)
        {
            var stack = new Stack<Node<Token>>();

            foreach (var token in tokens)
            {
                var node = new Node<Token>(token);

                if (token.Type == Token.TYPE.BINARY_OPERATOR)
                {
                    node.Right = stack.Pop();
                    node.Left = stack.Pop();
                }
                if (token.Type == Token.TYPE.FUNCTION)
                {
                    if (stack.TryPop(out var k))
                        node.Right = k;
                }
                if (token.Type == Token.TYPE.L_BRACE)
                {
                    continue;
                }

                stack.Push(node);
            }

            tree = stack.Pop();
            return EvaluateExpression(tree);

        }

        //to do: switch case -> dict
        static double EvaluateExpression(Node<Token> node)
        {
            if (node == null)
            {
                return 0;
            }

            if (node.Value.Type == Token.TYPE.FLOAT_NUM ||
                node.Value.Type == Token.TYPE.INT_NUM)
            {
                return double.Parse(node.Value.TokenString);
            }

            double leftValue = EvaluateExpression(node.Left);
            double rightValue = EvaluateExpression(node.Right);

            switch (node.Value.TokenString)
            {
                case "sin":
                    return Math.Sin(rightValue);
                case "cos":
                    return Math.Cos(rightValue);
                case "tan":
                    return Math.Tan(rightValue);
                case "ctg":
                    return 1 / Math.Tan(rightValue);
                case "^":
                    return Math.Pow(leftValue, rightValue);
                case "*":
                    return leftValue * rightValue;
                case "/":
                    return leftValue / rightValue;
                case "+":
                    return leftValue + rightValue;
                case "%":
                    return leftValue % rightValue;
                case "-":
                    return leftValue - rightValue;
                case "ln":
                    return Math.Log(rightValue);
                case "sqrt":
                    return Math.Sqrt(rightValue);
                case "exp":
                    return Math.Exp(rightValue);
                default:
                    throw new UnreachableException();
            }
        }
        //to do: parse constants
        private Token[] Transform(Dictionary<char, double> variables)
        {
            var res = new Token[Postfix.Length];
            Array.Copy(Postfix, res, res.Length);
            for (var i = 0; i < res.Length; i++)
            {
                if (res[i].Type == Token.TYPE.VARIABLE)
                {
                    var value = variables[res[i].TokenString[0]].ToString();
                    res[i] = new Token(value, Token.TYPE.FLOAT_NUM, Token.NUMBER_PRECENDENCY);
                }
            }

            return res;
        }
    }

}
