namespace ClassLibrary1;
public partial class Token
{
    /// <summary>
    /// Типы токенов
    /// </summary>
    public enum TYPE
    {
        BINARY_OPERATOR, // оператор бинарный
        UNARY_OPERATOR, // унарный оператор (унарный минус)
        INT_NUM, // целое число
        FLOAT_NUM, // число с плав запятой
        FUNCTION, // функция
        L_BRACE, //левая скобка
        R_BRACE, //правая скобка
        CONSTANT,
        VARIABLE // переменная 
    }
}
