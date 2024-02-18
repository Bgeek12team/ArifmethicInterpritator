namespace MyZmei;

public class Token(string stringValue, TokenType type)
{
    public string StringValue { get; init; } = stringValue;
    public TokenType Type { get; init; } = type;

    private static readonly Dictionary<string, TokenType> tokens = new() {
        {"var", TokenType.DECLARATION},
        {"const", TokenType.DECLARATION},
        {"положим", TokenType.DECLARATION},
        {"равным", TokenType.ASSIGNMENT_OPERATOR },
        {"равно", TokenType.ASSIGNMENT_OPERATOR },
        {"=", TokenType.ASSIGNMENT_OPERATOR },
        {"!", TokenType.SEPARATOR },
        {"?", TokenType.SEPARATOR },
        {".", TokenType.DOT }
    };

    private static readonly List<string> functions = 
        [CalculateFunction.StringValue,
        PrintFunction.StringValue,
        DNFFunction.StringValue,
        KNFFunction.StringValue, 
        MinFunction.StringValue,
            SortFunction.StringValue];

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
