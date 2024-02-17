namespace ClassLibrary1;
/// <summary>
/// Класс, переводящий массив токенов в инверсную польскую запись
/// </summary>
public static class Polish
{
    /// <summary>
    /// Переводит массив токенов в инверсную польскую запись
    /// </summary>
    /// <param name="expression">Массив токенов</param>
    /// <returns>Массив токенов в инверсной польской записи</returns>
    public static Token[] ToInversePolishView(Token[] expression)
    {
        var output = new List<Token>();
        var operators = new Stack<Token>();

        foreach (var token in expression)
        {
            if (token.IsNumber() || token.IsBoolean())
                output.Add(token);

            else if (token.Type == Token.TYPE.BINARY_OPERATOR ||
                     token.Type == Token.TYPE.FUNCTION ||
                     token.Type == Token.TYPE.ARIPTHMETIC_BOOLEAN_OPERATOR ||
                     token.Type == Token.TYPE.BOOLEAN_BOOLEAN_OPERATOR ||
                     token.Type == Token.TYPE.BOOLEAN_FUNCTION)
            {
                while (operators.Count > 0 &&
                    token.Precendency <= operators.Peek().Precendency)
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
            else if (token.Type == Token.TYPE.L_BRACE)
                operators.Push(token);

            else if (token.Type == Token.TYPE.R_BRACE)
            {
                while (operators.Count > 0 &&
                       operators.Peek().Type != Token.TYPE.L_BRACE)
                {
                    output.Add(operators.Pop());
                }
                operators.TryPop(out _);
            }
        }

        while (operators.Count > 0)
        {
            output.Add(operators.Pop());
        }

        return output.ToArray();

    }
}