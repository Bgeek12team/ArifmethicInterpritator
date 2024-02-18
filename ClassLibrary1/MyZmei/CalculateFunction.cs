namespace MyZmei;

public static class CalculateFunction
{
    public static string StringValue = "чему_равно";
    public static string CalculateResult(string exp, Dictionary<VariableName, VariableValue> args)
    {
        var resExp = new ClassLibrary1.Expression(exp);

        if (resExp.ParseTree())
            return resExp.CalculateAt([], []).ToString();
        else
            return "хуйня! переменные говна";
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
