using System;
using System.Collections.Generic;
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
        public double CalculateAt(Dictionary<char, double> variables) 
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
                    return double.NaN;
                }
            }

            
            var resultArr = Transform(variables);
            var res = CalculateInverse(resultArr);
            
            resultArr = null;

            return res;
        }

        // вычислить из обратной записи значение
        private static double CalculateInverse(Token[] tokens)
        {
            var stack = new Stack<double>();

            foreach (var token in tokens)
            {
                if (token.Type == Token.TYPE.INT_NUM ||
                    token.Type == Token.TYPE.FLOAT_NUM)
                {
                    stack.Push(double.Parse(token.TokenString));
                }
                else
                {
                    switch (token.TokenString)
                    {
                        case "sin":
                            stack.Push(Math.Sin(stack.Pop()));
                            break;
                        case "cos":
                            stack.Push(Math.Cos(stack.Pop()));
                            break;
                        case "tan":
                            stack.Push(Math.Tan(stack.Pop()));
                            break;
                        case "ctg":
                            stack.Push(1 / Math.Tan(stack.Pop()));
                            break;
                        case "^":
                            double exponent = stack.Pop();
                            double baseNum = stack.Pop();
                            stack.Push(Math.Pow(baseNum, exponent));
                            break;
                        case "*":
                            stack.Push(stack.Pop() * stack.Pop());
                            break;
                        case "/":
                            double divisor = stack.Pop();
                            double dividend = stack.Pop();
                            stack.Push(dividend / divisor);
                            break;
                        case "+":
                            stack.Push(stack.Pop() + stack.Pop());
                            break;
                        case "%":
                            stack.Push((int)stack.Pop() % (int)stack.Pop());
                            break;
                        case "-":
                            double subtrahend = stack.Pop();
                            double minuend = stack.Pop();
                            stack.Push(minuend - subtrahend);
                            break;
                        case "ln":
                            stack.Push(Math.Log(stack.Pop()));
                            break;
                        case "sqrt":
                            stack.Push(Math.Sqrt(stack.Pop()));
                            break;
                        case "exp":
                            stack.Push(Math.Exp(stack.Pop()));
                            break;
                    }
                }
            }

            return stack.Pop();
        }

        private Token[] Transform(Dictionary<char, double> variables) 
        {
            var res = new Token[Postfix.Length];
            Array.Copy(Postfix, res, res.Length);
            for(var i = 0; i < res.Length; i++)
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
