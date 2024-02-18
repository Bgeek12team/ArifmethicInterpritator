namespace MyZmei;

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
