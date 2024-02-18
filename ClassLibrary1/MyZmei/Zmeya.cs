namespace MyZmei;

public class Zmeya
{
    internal static Dictionary<VariableName, VariableValue> variables = [];
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
    string ReplaceVariables(string str)
    {
        bool flag = false;
        while (!flag)
        {
            flag = true;
            foreach (var key in variables.Keys)
            {
                if (str.Contains($"{key.Name}"))
                {
                    str = str.Replace(key.Name, " " + variables[key].Value + " ");
                    flag = false;
                }
            }
        }
        return str;
    }
    string Evaluate(Expression expr)
    {
        if (expr.Tokens.Length < 1)
            return "плиз покорми змею выражениями";
        if (expr.Tokens[0].StringValue == CalculateFunction.StringValue)
        {
            var str = CalculateFunction.TrimSelf(expr);
            str = ReplaceVariables(str);
            return CalculateFunction.CalculateResult(str, variables);
        }
        else if (expr.Tokens[0].StringValue == PrintFunction.StringValue)
        {
            var str = PrintFunction.TrimSelf(expr);
            str = ReplaceVariables(str);
            return PrintFunction.CalculateResult(str);
        }
        else if (expr.Tokens[0].StringValue == DNFFunction.StringValue)
        {
            var str = DNFFunction.TrimSelf(expr);
            str = ReplaceVariables(str);
            return DNFFunction.CalculateResult(str);
        }
        else if (expr.Tokens[0].StringValue == KNFFunction.StringValue)
        {
            var str = KNFFunction.TrimSelf(expr);
            str = ReplaceVariables(str);
            return KNFFunction.CalculateResult(str);
        }
        else if (expr.Tokens[0].StringValue == MinFunction.StringValue)
        {
            var str = MinFunction.TrimSelf(expr);
            str = ReplaceVariables(str);
            return MinFunction.CalculateResult(str);
        }
        else if (expr.Tokens[0].StringValue == SortFunction.StringValue)
        {
            var str = SortFunction.TrimSelf(expr);
            str = ReplaceVariables(str);
            return SortFunction.CalculateResult(str);
        }
        return "не хайп не шарю";
    }

    string ParseAssignment(Expression expr)
    {
        if (expr.Tokens.Length < 4)
            return "присваивание через провозгласить делается, ты долбаеб?";

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
