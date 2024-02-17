using ClassLibrary1;

var expression = "x > y | y < e";
var tokens = Token.Tokenize(expression);
var inverse = Polish.ToInversePolishView(tokens);
foreach (var token in inverse)
    Console.WriteLine(token);

var exp = new Expression(expression);
var variables = new Dictionary<char, double>()
{
    {'x', 12 },
    {'y', 2 },
    {'z', 10 }
};
var result = exp.CalculateAt(variables);
if (exp.IsBooleanExpression)
    Console.WriteLine((bool)result);
else
    Console.WriteLine((double)result);

