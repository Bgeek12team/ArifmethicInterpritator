using Microsoft.VisualBasic;
using System.Linq;
using System.Xml.Linq;
using ClassLibrary1;
using System;
namespace MyZmei;
public enum TokenType
{
    DECLARATION,
    VARIABLE_NAME,
    ASSIGNMENT_OPERATOR,
    VARIABLE_VALUE,
    SEPARATOR,
    DOT,
    FUNCTION
}

public static class CalculateFunction
{
    public static string StringValue = "чему_равно";
    public static string CalculateResult(string exp, Dictionary<VariableName, VariableValue> args)
    {
        var booleanVars = new Dictionary<string, bool>();
        var numericVars = new Dictionary<string, double>();

        foreach(var variable in args)
        {
            if (variable.Value.GetVariableType() == VariableTypes.BOOLEAN)
                booleanVars.Add(variable.Key.Name, Convert.ToBoolean(variable.Value.Value));
            if (variable.Value.GetVariableType() == VariableTypes.NUMBER)
                numericVars.Add(variable.Key.Name, Convert.ToDouble(variable.Value.Value));
        }

        var resExp = new ClassLibrary1.Expression(exp);

        return resExp.CalculateAt(numericVars, booleanVars).ToString();
    }
    public static string TrimSelf(Expression tokens)
    {
        string res = "";

        foreach (var tk in tokens.Tokens)
        {
            if (tk.Type != TokenType.FUNCTION)
                res += tk.StringValue + " ";
        }
        return res.Trim();
    }
}

public static class PrintFunction
{
    public static string StringValue = "отобрази";
    public static string CalculateResult(string exp) => exp;

    public static string TrimSelf(Expression tokens)
    {
        string res = "";
        foreach(var tk in tokens.Tokens)
        {
            if (tk.Type != TokenType.FUNCTION)
                res += tk.StringValue + " ";
        }
        return res.Trim();
    }
}
public class Token(string stringValue, TokenType type)
{
    public string StringValue { get; init; } = stringValue;
    public TokenType Type { get; init; } = type;

    private static readonly Dictionary<string, TokenType> tokens = new() {
        {"провозгласить", TokenType.DECLARATION},
        {"равным", TokenType.ASSIGNMENT_OPERATOR },
        {"!", TokenType.SEPARATOR },
        {"?", TokenType.SEPARATOR },
        {".", TokenType.DOT }
    };

    private static readonly List<string> functions = 
        [CalculateFunction.StringValue,
        PrintFunction.StringValue];

    public static Token[] Tokenize(string str)
    {
        var res = new List<Token>();
        var start = 0;
        while (start < str.Length)
        {

            if (Char.IsWhiteSpace(str[start]))
            {
                start++;
                continue;
            }
            string tokenStr;
            var found = false;
            foreach (var tk in Token.tokens.Keys)
            {
                if (str[start..].StartsWith(tk))
                {
                    if (start + tk.Length >= str.Length)
                        continue;
                    tokenStr = str[start..(start + tk.Length)];
                    res.Add(new(tokenStr.Trim(), tokens[tokenStr]));
                    found = true;
                    start = start + tk.Length;
                }
                if (found) break; 
            }
            if (found) continue;

            found = false;
            foreach (var fun in functions)
            {
                if (str[start..].StartsWith(fun))
                {
                    if (start + fun.Length >= str.Length)
                        continue;
                    tokenStr = str[start..(start + fun.Length)];
                    res.Add(new(tokenStr.Trim(), TokenType.FUNCTION));
                    start = start + fun.Length;
                    found = true;
                }
                if (found) break;
            }
            if (found) continue;
            var end = start;
            while (end < str.Length && !Char.IsWhiteSpace(str[end])) end++;

            tokenStr = str[start.. end ];
            if (res.Count > 0 && res[^1].Type == TokenType.ASSIGNMENT_OPERATOR)
            {
                res.Add(new(tokenStr.Trim(), TokenType.VARIABLE_VALUE));
            }
            else
            {
                res.Add(new(tokenStr.Trim(), TokenType.VARIABLE_NAME));
            }
            start = end;
        }

        return [.. res];
    }
}

public enum VariableTypes
{
    BOOLEAN, 
    NUMBER,
    STRING
}

public record VariableName(string Name);

public record VariableValue(string Value)
{
    public VariableTypes GetVariableType()
    {
        if (bool.TryParse(Value, out _)) return VariableTypes.BOOLEAN;
        if (double.TryParse(Value, out _)) return VariableTypes.NUMBER;
        return VariableTypes.STRING;
    }
}

public enum ExpressionTypes
{
    ASSIGNMENT,
    VOID
}
public record Expression
{
    public ExpressionTypes ExpressionType { get; init; }

    public Token[] Tokens { get; init; }
    public Expression(Token[] tokenExpr)
    {
        Tokens = tokenExpr;
        foreach(var token in tokenExpr)
        {
            if (token.Type == TokenType.ASSIGNMENT_OPERATOR)
            {
                this.ExpressionType = ExpressionTypes.ASSIGNMENT;
                return;
            }
        }
        this.ExpressionType = ExpressionTypes.VOID;
    }
};
public class Zmeya
{
    private Dictionary<VariableName, VariableValue> variables = []; 
    public string EvaluateExpression(Expression expr) 
    {
        if (expr.ExpressionType == ExpressionTypes.ASSIGNMENT)
        {
            return ParseAssignment(expr);
        }
        else
        {
            return Evaluate(expr);
        }
            
    }

    string Evaluate(Expression expr)
    {
        if (expr.Tokens[0].StringValue == CalculateFunction.StringValue)
        {
            var str = CalculateFunction.TrimSelf(expr);
            foreach(var key in variables.Keys)
            {
                if (key.Name == str &&
                    variables[key].GetVariableType() == VariableTypes.STRING)
                    str = str.Replace(str, variables[key].Value);
            }
            return CalculateFunction.CalculateResult(str, variables);
        }
        else if (expr.Tokens[0].StringValue == PrintFunction.StringValue)
        {
            var str = PrintFunction.TrimSelf(expr);
            foreach (var key in variables.Keys)
            {
                if (key.Name == str)
                    str = str.Replace(str, variables[key].Value);
            }
            return PrintFunction.CalculateResult(str);
        }
        return "че за хуйня?";
    }

    string ParseAssignment(Expression expr)
    {
        if (expr.Tokens.Length < 4)
            return "хуйня!";

        var potentialName = new VariableName(expr.Tokens[1].StringValue);
        var assignedString = "";
        for (int i = 3; i < expr.Tokens.Length; i++)
        {
            if (expr.Tokens[i].Type == TokenType.SEPARATOR)
                break;
            assignedString += expr.Tokens[i].StringValue + " ";
        }
        var value = new VariableValue(assignedString);
        var found = false;
        foreach (var name in variables.Keys)
        {
            if (name.Equals(potentialName))
            {
                variables[name] = value;
                found = true;
            }
        }
        if (found)
            return $"нет, и будет {potentialName.Name} равным {value.Value}";
        variables.Add(potentialName, value);
        return $"да будет {potentialName.Name} равным {value.Value}";

    }
}

public static class Z_Python
{
    static Zmeya context = new();
    static Stack<Expression> history = new();
    public static string Eval(string str)
    {
        var tokens = Token.Tokenize(str);
        var exp = new Expression(tokens);
        history.Push(exp);
        return context.EvaluateExpression(exp);
    }
}