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
    /// Массив токенов в постфиксной записи
    /// </summary>
    private Token[]? Postfix { get; set; }
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
        if (Postfix == null)
            try
            {
                var tokens = Token.Tokenize(StringExpression);
                Postfix = Polish.ToInversePolishView(tokens);
                Transform();
                treeNode = CalculateTree(Postfix);
            }
            catch
            {
                return double.NaN;
            }

        var res = EvaluateExpression(treeNode, variables);

        return res;
    }
    private static Node<Token> CalculateTree(Token[] tokens)
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

    static double EvaluateExpression(Node<Token> node, Dictionary<char, double> variables)
    {
        if (node == null)
            return 0;

        if (node.Value.Type == Token.TYPE.VARIABLE)
        {
            return variables[node.Value.TokenString.ToString()[0]];
        }

        if (node.Value.IsNumber())
            return double.Parse(node.Value.TokenString);

        double leftValue = EvaluateExpression(node.Left, variables);
        double rightValue = EvaluateExpression(node.Right, variables);

        if (node.Value.Type == Token.TYPE.FUNCTION)
            return Functions[node.Value.TokenString](rightValue);
        else
            return BinaryOperators[node.Value.TokenString](leftValue, rightValue);
    }

    private void Transform()
    {
        for (var i = 0; i < Postfix.Length; i++)
        {
            if (Postfix[i].Type == Token.TYPE.CONSTANT)
            {
                var value = Constants[Postfix[i].TokenString].ToString();
                Postfix[i] = new Token(value, Token.TYPE.FLOAT_NUM, Token.NUMBER_PRECENDENCY);
            }
        }

    }
}

