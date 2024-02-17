namespace ClassLibrary1;
public partial class Token
{
    /// <summary>
    /// Типы токенов
    /// </summary>
    public enum TYPE
    {
        /// <summary>
        /// оператор бинарный
        /// </summary>
        BINARY_OPERATOR,
        /// <summary>
        /// унарный оператор (унарный минус)
        /// </summary>
        UNARY_OPERATOR,
        /// <summary>
        /// целое число
        /// </summary>
        INT_NUM,
        /// <summary>
        /// число с плавающей запятой
        /// </summary>
        FLOAT_NUM,
        /// <summary>
        /// функция
        /// </summary>
        FUNCTION,
        /// <summary>
        /// левая скобка
        /// </summary>
        L_BRACE,
        /// <summary>
        /// правая скобка
        /// </summary>
        R_BRACE,
        /// <summary>
        /// математическая константа
        /// </summary>
        CONSTANT,
        /// <summary>
        /// переменная
        /// </summary>
        VARIABLE,
        /// <summary>
        /// булевый оператор
        /// </summary>
        ARIPTHMETIC_BOOLEAN_OPERATOR,
        BOOLEAN_BOOLEAN_OPERATOR,
        BOOLEAN_FUNCTION,
        BOOLEAN_CONSTANT
    }
}