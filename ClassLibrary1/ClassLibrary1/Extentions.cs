namespace ClassLibrary1;

public static class Extentions
{
    /// <summary>
    /// Возвращает факториал числа
    /// </summary>
    /// <param name="a">Число</param>
    /// <returns>Факториал числа</returns>
    public static long Factorial(int a)
    {
        int res = 1;
        while (a > 0)
        {
            res *= a;
            a--;
        }
        return res;
    }

}
