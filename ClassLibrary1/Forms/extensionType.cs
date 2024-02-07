using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms
{
    static class extensionType
    {
        public static int count(this string str, char elem)
        {
            return str.Count(f => f == (elem));
        }
        public static void blinkColor(this Control control, Color color)
        {
            Color originalColor = control.BackColor;
            var changeBlink = new Thread(() => control.BackColor = color);
            var changeOriginal = new Thread(() => { Thread.Sleep(200); control.BackColor = originalColor; });
            changeBlink.Start();
            changeOriginal.Start();
        }
    }
}
