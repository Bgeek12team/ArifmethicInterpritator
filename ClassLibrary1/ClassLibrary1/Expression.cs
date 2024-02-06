using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Expression(string expression = null!)
    {
        private Token[]? Postfix { get; set; }
        public string StringExpression { get; init; } = expression;

        public double CalculateAt(params (char variable, double value)[] variables) 
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

            return CalculateInverse(Transform(Postfix, variables));
        }

        // вычислить из обратной записи значение
        private double CalculateInverse(Token[] postfix)
        {
            throw new NotImplementedException();
        }

        // все переменные заменить в соответсвии с массивом туплей
        private Token[] Transform(Token[] tokens, (char variable, double value)[] variables) 
        { 
             throw new NotImplementedException();
        }
    }

}
