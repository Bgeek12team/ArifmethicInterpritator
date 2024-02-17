namespace ClassLibrary1;
public static class Extentions
{
    /// <summary>
    /// Возвращает факториал числа
    /// </summary>
    /// <param name="a">Число</param>
    /// <returns>Факториал числа</returns>
    public static long Factorial (int a)
    {
        int res = 1;
        while (a > 0)
        {
            res *= a;
            a--;
        }
        return res;
    }
}
/// <summary>
/// Арифметическое выражение, значение которого можно вычислить
/// при данных значениях переменных
/// </summary>
/// <param name="expression">Строковое представление выражения</param>
public class Expression(string expression)
{
    /// <summary>
    /// Словарь, связывающий фукнции и их вычисление
    /// </summary>
    private static readonly Dictionary<string, Func<double, double>> Functions = new()
    {
        {"sin", Math.Sin },
        {"cos", Math.Cos },
        {"sqrt", Math.Sqrt },
        {"exp", Math.Exp },
        {"tg", Math.Tan },
        {"ctg", (x) => 1 / Math.Tan(x) },
        {"ln", Math.Log },
        {"-", x => -x },
        {"fact", x => Extentions.Factorial((int)x)}
    };
    /// <summary>
    /// Словарь, связывающий бинарные операторы и их вычисление
    /// </summary>
    private static readonly Dictionary<string, Func<double, double, double>> BinaryOperators = new()
    {
        {"+", (a, b) => a + b },
        {"-", (a, b) => a - b },
        {"*", (a, b) => a * b },
        {"/", (a, b) => a / b },
        {"^", Math.Pow },
        {"%", (a, b) => a % b },
    };
    /// <summary>
    /// Словарь, связывающий константы и их значения
    /// </summary>
    private readonly Dictionary<string, double> Constants = new()
    {
        {"e", Math.E },
        {"pi", Math.PI }
    };
    /// <summary>
    /// Верхняя вершина дерева парсинга
    /// </summary>
    private Node<Token> treeNode;
    /// <summary>
    /// Выражение в строковом виде
    /// </summary>
    public string StringExpression { get; init; } = expression;

    public double CalculateAt(Dictionary<char, double> variables)
    {
        if (treeNode == null)
            try
            {
                var tokens = Token.Tokenize(StringExpression);
                var postfix = Polish.ToInversePolishView(tokens);
                Transform(postfix);
                treeNode = CalculateTree(postfix);
            }
            catch
            {
                return double.NaN;
            }

        var res = EvaluateExpression(treeNode, variables);

        return res;
    }
<<<<<<< Updated upstream
    private static Node<Token> CalculateTree(Token[] tokens)
=======

    public object CalculateAt(Dictionary<string, double> variables
            ,Dictionary<string, bool> booleanVariables)
    {
        if (!ParseTree())
            return double.NaN;
        if (!IsBooleanExpression)
        {
            var res = EvaluateAriphmeticExpression(TreeNode, variables);

            return res;
        }
        else
        {
            var res = EvaluateBooleanExpression(TreeNode, variables, booleanVariables);

            return res;
        }
    }
    /// <summary>
    /// строит дерево парсинга по массиву токенов в постфиксной записи
    /// </summary>
    /// <param name="tokens">массив токенов в постификной записи</param>
    /// <returns>верхняя вершина дерева парсинга</returns>
    /// <exception cref="Exception">исключение возникающее 
    /// если значение выражения невозможно вычислить</exception>
    private Node<Token> BuildTree(Token[] tokens)
>>>>>>> Stashed changes
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
                if (stack.TryPop(out var k))
                    node.Right = k;

            if (token.Type == Token.TYPE.L_BRACE)
                continue;

            stack.Push(node);
        }

        if (stack.TryPop(out var res))
            return res;
        throw new Exception("Невозможно вычислить значение");
    }
<<<<<<< Updated upstream

    static double EvaluateExpression(Node<Token> node, Dictionary<char, double> variables)
=======
    /// <summary>
    /// вычисляет значение дерева парсинга по словарю переменных-значений
    /// </summary>
    /// <param name="node">вершина дерева парсинга</param>
    /// <param name="variables">словарь переменных-значений/param>
    /// <returns>значение дерева парсинга</returns>
    static object EvaluateAriphmeticExpression(Node<Token> node, Dictionary<string, double> variables)
>>>>>>> Stashed changes
    {
        if (node == null)
            return 0;

        if (node.Value.Type == Token.TYPE.VARIABLE)
        {
            return variables[node.Value.TokenString];
        }

        if (node.Value.IsNumber())
            return double.Parse(node.Value.TokenString);

        double leftValue = EvaluateExpression(node.Left, variables);
        double rightValue = EvaluateExpression(node.Right, variables);

        if (node.Value.Type == Token.TYPE.FUNCTION)
<<<<<<< Updated upstream
            return Functions[node.Value.TokenString](rightValue);
        else
            return BinaryOperators[node.Value.TokenString](leftValue, rightValue);
=======
            return Functions[node.Value.TokenString](Convert.ToDouble(rightValue));
        else
            return BinaryOperators[node.Value.TokenString](Convert.ToDouble(leftValue),
                Convert.ToDouble(rightValue));
    }
    static object EvaluateBooleanExpression
        (Node<Token> node,
        Dictionary<string, double> variables,
        Dictionary<string, bool> booleanVariables)
    {
        if (node == null)
            return 0;

        if (node.Value.Type == Token.TYPE.VARIABLE)
        {
            var variable = node.Value.TokenString;
            if (variables.TryGetValue(variable, out double value))
                return value;
            else
                return booleanVariables[variable];
        }

        if (node.Value.IsNumber())
        {
            return double.Parse(node.Value.TokenString);
        }

        if (node.Value.IsBoolean())
        {
            return bool.Parse(node.Value.TokenString);
        }

        object leftValue = EvaluateBooleanExpression(node.Left, variables, booleanVariables);
        object rightValue = EvaluateBooleanExpression(node.Right, variables, booleanVariables);

        if (node.Value.Type == Token.TYPE.FUNCTION)
            return Functions[node.Value.TokenString](Convert.ToDouble(rightValue));
        else if (node.Value.Type == Token.TYPE.BOOLEAN_FUNCTION)
            return BooleanFunctions[node.Value.TokenString]((bool)rightValue);
        else if (node.Value.Type == Token.TYPE.BINARY_OPERATOR)
            return BinaryOperators[node.Value.TokenString](Convert.ToDouble(leftValue)
                , Convert.ToDouble(rightValue));
        else if (node.Value.Type == Token.TYPE.ARIPTHMETIC_BOOLEAN_OPERATOR)
            return BooleanOperators[node.Value.TokenString](Convert.ToDouble(leftValue)
                , Convert.ToDouble(rightValue));
        else if (node.Value.Type == Token.TYPE.BOOLEAN_BOOLEAN_OPERATOR)
            return BooleanBooleanOperators[node.Value.TokenString]((bool)leftValue, (bool)rightValue);
        else
            return double.NaN;
>>>>>>> Stashed changes
    }

    private void Transform(Token[] postfix)
    {
        for (var i = 0; i < postfix.Length; i++)
        {
            if (postfix[i].Type == Token.TYPE.CONSTANT)
            {
                var value = Constants[postfix[i].TokenString].ToString();
                postfix[i] = new Token(value, Token.TYPE.FLOAT_NUM, Token.NUMBER_PRECENDENCY);
            }
        }

    }
}

