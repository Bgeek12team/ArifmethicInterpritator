using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZmei;

public static class KNFFunction
{
    public static string StringValue = "лнр";
    public static string CalculateResult(string exp) => "бля куда ты";

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
