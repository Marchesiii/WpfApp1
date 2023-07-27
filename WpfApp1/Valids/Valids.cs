using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class Valids
    {
        public static bool CheckStringNullOrEmpty(string value) => string.IsNullOrEmpty(value);
        public static bool CheckIsPositive(int value) => int.IsPositive(value);

        public static void DisplayValidationError(string message) => MessageBox.Show(message);
    }
}
