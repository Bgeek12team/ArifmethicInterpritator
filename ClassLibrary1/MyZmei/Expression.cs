namespace MyZmei;

public record Expression
{
    public ExpressionTypes ExpressionType { get; init; }

    public Token[] Tokens { get; init; }
    public Expression(Token[] tokenExpr)
    {
        var resList = new List<Token>();
        foreach (var token in tokenExpr)
        {
            if (token.Type != TokenType.SEPARATOR)
                resList.Add(token);
            else
                break;
        }
        Tokens = [.. resList];
        foreach(var token in Tokens)
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
