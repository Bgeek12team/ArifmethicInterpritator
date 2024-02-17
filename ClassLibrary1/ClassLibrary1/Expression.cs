namespace ClassLibrary1;
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
    /// Словарь, связывающий бинарные операторы и их вычисление
    /// </summary>
    private static readonly Dictionary<string, Func<double, double, bool>> BooleanOperators = new()
    {
        {">", (a, b) => a > b },
        {"<", (a, b) => a < b },
        {"=", (a, b) => a == b }
    }; 
    private static readonly Dictionary<string, Func<bool, bool, bool>> BooleanBooleanOperators = new()
    {
        {"&", (a, b) => a && b },
        {"|", (a, b) => a || b },
    };
    private static readonly Dictionary<string, Func<bool, bool>> BooleanFunctions = new()
    {
        {"!", a => !a }
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
    public Node<Token> TreeNode { get; private set; }
    /// <summary>
    /// Выражение в строковом виде
    /// </summary>
    public string StringExpression { get; init; } = expression;
    /// <summary>
    /// Вычисляет значение выражения при заданных перменных
    /// </summary>
    /// <param name="variables">Словарь, связывающий перменные и их значения</param>
    /// <returns>Значение выражения при заданных перменных</returns>
    public bool IsBooleanExpression { get; private set; } = false;

    public object CalculateAt(Dictionary<char, double> variables
            ,Dictionary<char, bool> booleanVariables)
    {
        if (TreeNode == null)
            try
            {
                var tokens = Token.Tokenize(StringExpression);
                var postfix = Polish.ToInversePolishView(tokens);
                Transform(postfix);
                TreeNode = CalculateTree(postfix);
            }
            catch
            {
                return double.NaN;
            }
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
    private Node<Token> CalculateTree(Token[] tokens)
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


            if (token.Type == Token.TYPE.BOOLEAN_FUNCTION)
            {
                IsBooleanExpression = true;
                if (stack.TryPop(out var k))
                    node.Right = k;
            }

            if (token.Type == Token.TYPE.L_BRACE)
                continue;

            if (token.Type == Token.TYPE.ARIPTHMETIC_BOOLEAN_OPERATOR ||
                token.Type == Token.TYPE.BOOLEAN_BOOLEAN_OPERATOR)
            {
                IsBooleanExpression = true;
                node.Right = stack.Pop();
                node.Left = stack.Pop();
            }

            stack.Push(node);
        }

        if (stack.TryPop(out var res))
            return res;
        throw new Exception("Невозможно вычислить значение");
    }
    /// <summary>
    /// вычисляет значение дерева парсинга по словарю переменных-значений
    /// </summary>
    /// <param name="node">вершина дерева парсинга</param>
    /// <param name="variables">словарь переменных-значений/param>
    /// <returns>значение дерева парсинга</returns>
    static object EvaluateAriphmeticExpression(Node<Token> node, Dictionary<char, double> variables)
    {
        if (node == null)
            return 0;

        if (node.Value.Type == Token.TYPE.VARIABLE)
        {
            return variables[node.Value.TokenString.ToString()[0]];
        }

        if (node.Value.IsNumber())
            return double.Parse(node.Value.TokenString);

        object leftValue = EvaluateAriphmeticExpression(node.Left, variables);
        object rightValue = EvaluateAriphmeticExpression(node.Right, variables);

        if (node.Value.Type == Token.TYPE.FUNCTION)
            return Functions[node.Value.TokenString]((double)rightValue);
        else if (node.Value.Type == Token.TYPE.BOOLEAN_FUNCTION)
            return BooleanFunctions[node.Value.TokenString]((bool)rightValue);
        else
            return BinaryOperators[node.Value.TokenString]((double)leftValue, (double)rightValue);
    }
    static object EvaluateBooleanExpression
        (Node<Token> node,
        Dictionary<char, double> variables,
        Dictionary<char, bool> booleanVariables)
    {
        if (node == null)
            return 0;

        if (node.Value.Type == Token.TYPE.VARIABLE)
        {
            var variable = node.Value.TokenString[0];
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
            return Functions[node.Value.TokenString]((double)rightValue);
        else if (node.Value.Type == Token.TYPE.BOOLEAN_FUNCTION)
            return BooleanFunctions[node.Value.TokenString]((bool)rightValue);
        else if (node.Value.Type == Token.TYPE.BINARY_OPERATOR)
            return BinaryOperators[node.Value.TokenString]((double)leftValue, (double)rightValue);
        else if (node.Value.Type == Token.TYPE.ARIPTHMETIC_BOOLEAN_OPERATOR)
            return BooleanOperators[node.Value.TokenString]((double)leftValue, (double)rightValue);
        else
            return BooleanBooleanOperators[node.Value.TokenString]((bool)leftValue, (bool)rightValue);

    }
    /// <summary>
    /// Преобразует массив токенов, заменяя константы фактическими значениямии
    /// </summary>
    /// <param name="postfix">массив токенов</param>
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

