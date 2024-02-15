﻿using ClassLibrary1;

var expression = "-x + y";
var tokens = Token.Tokenize(expression);
var inverse = Polish.ToInversePolishView(tokens);
foreach (var token in inverse)
    Console.WriteLine(token);

var exp = new Expression(expression);
var variables = new Dictionary<char, double>()
{
    {'x', 12 },
    {'y', 2 }
};

//Console.WriteLine(exp.CalculateAt(variables, out _));
