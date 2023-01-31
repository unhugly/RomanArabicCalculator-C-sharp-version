using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorRoman
{
    public class RomanConverter
    {
        public IList<string> RomansNumerals { get; } = new List<string> { "c", "xc", "l", "xl", "x", "ix", "v", "iv", "i" };
        public IList<int> ArabicsNumerals { get; } = new List<int> { 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        public IList<string> RomansDictionary { get; }

        public RomanConverter() {
            Console.WriteLine("Loading..");
            RomansDictionary = new List<string>();
            for (int i = 0; i <= 100; ++i) // 100 - максимально возможный ответ при выполнении арифметических операций в данном ТЗ
                RomansDictionary.Add(InitializeRomansValues(i));
            Console.WriteLine("Load complete!");
        }

        private string InitializeRomansValues(int arabic) {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ArabicsNumerals.Count; ++i)
                while (arabic >= ArabicsNumerals[i]) {
                    arabic -= ArabicsNumerals[i];
                    result.Append(RomansNumerals[i]);
                }
            return result.ToString().ToUpper();
        }

        public string A2R(int arabic) {
            if (arabic == 0) return "N(0)";
            else return RomansDictionary[arabic];
        }

        public int R2A(string roman) {
            return RomansDictionary.IndexOf(roman.ToUpper());
        }

    }
}
