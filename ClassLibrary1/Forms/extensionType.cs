using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms
{
    static class ExtensionType
    {
        public static int Count(this string str, char elem) => str.Count(f => f == (elem));
        public static bool IsNumeric(this string num) => double.TryParse(num, out _);
        public static bool IsBoolean(this string str) => bool.TryParse(str, out _);
        public static void BlinkColor(this Control control, Color color)
        {
            Color originalColor = control.BackColor;
            var changeBlink = new Thread(() => control.BackColor = color);
            var changeOriginal = new Thread(() => { Thread.Sleep(200); control.BackColor = originalColor; });
            changeBlink.Start();
            changeOriginal.Start();
        }
        public static int ChekCorrect(this string expression)
        {
            if (expression.Count('(') != expression.Count(')')) { return 0; }
            else { return -1; }
        }
        
    }
}
