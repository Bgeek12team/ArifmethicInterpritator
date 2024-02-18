namespace MyZmei;

public record VariableName(string Name)
{

    public VariableTypes GetVariableType()
    {
        var value = Zmeya.variables[this].Value;
        if (bool.TryParse(value, out _)) return VariableTypes.BOOLEAN;
        if (double.TryParse(value, out _)) return VariableTypes.NUMBER;
        return VariableTypes.STRING;
    }
}
