using ClassLibrary1;

var expression = "( x < 2) & z ";
var tokens = Token.Tokenize(expression);
var inverse = Polish.ToInversePolishView(tokens);
foreach (var token in inverse)
    Console.WriteLine(token);

var exp = new Expression(expression);
var variables = new Dictionary<char, double>()
{
    {'x', 1.17 },
    {'y', 0.17 }
};
var boolVariables = new Dictionary<char, bool>()
{
    {'z', true },
    {'w', false }
};
var result = exp.CalculateAt(variables, boolVariables);
if (exp.IsBooleanExpression)
    Console.WriteLine((bool)result);
else
    Console.WriteLine((double)result);

