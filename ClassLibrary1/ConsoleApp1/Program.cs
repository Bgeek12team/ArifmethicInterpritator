using ClassLibrary1;

var expression = "ln( cos(5 * 1) - 10 ) ";
var tokens = Token.Tokenize(expression);
var inverse = Polish.ToInversePolishView(tokens);
foreach (var token in inverse)
    Console.WriteLine(token);