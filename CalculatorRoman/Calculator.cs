using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorRoman
{
    class Calculator
    {
        public RomanConverter Converter { get; }

        public int MinInput { get; } = 1;
        public int MaxInput { get; } = 10;

        public Calculator() {
            Converter = new RomanConverter();
        }

        public string NextExpression(string str) {
            StringBuilder result = new StringBuilder();
            var exp = LineHandler.HandleExpression(str);
            Console.WriteLine("Calculating..");
            if (IsRoman(exp.Item1)) result.Append(CalculateRoman(exp));
            else result.Append(Calculate(exp));
            Console.WriteLine("Calculated!");
            return result.ToString();
        }

        private bool IsRoman(string str) {
            foreach (char c in str.ToCharArray())
                return Converter.RomansNumerals.Contains(c.ToString());
            return false;
        }

        private string CalculateRoman(Tuple<string,char,string> exp) {
            var tempTuple = new Tuple<string, char, string>(
                Converter.R2A(exp.Item1).ToString(),
                exp.Item2,
                Converter.R2A(exp.Item3).ToString());
            int result = int.Parse(Calculate(tempTuple));
            if (result < 0) throw new Exception("There are no negative numbers in the Roman numeral system");
            return Converter.A2R(result);
        }

        private string Calculate(Tuple<string, char, string> exp) {
            if (IsInRange(exp)) throw new Exception("Input must be in range from 1 to 10 || from I to X");
            string result = null;
            switch (exp.Item2) {
                case '+':
                    result = $"{int.Parse(exp.Item1) + int.Parse(exp.Item3)}";
                    break;
                case '-':
                    result = $"{int.Parse(exp.Item1) - int.Parse(exp.Item3)}";
                    break;
                case '*':
                    result = $"{int.Parse(exp.Item1) * int.Parse(exp.Item3)}";
                    break;
                case '/':
                    result = $"{int.Parse(exp.Item1) / int.Parse(exp.Item3)}";
                    break;
                default:
                    throw new Exception("Wrong operator");
            }
            return result;
        }

        private bool IsInRange(Tuple<string, char, string> exp)
        {
            return (int.Parse(exp.Item1) < MinInput || int.Parse(exp.Item1) > MaxInput)
                || (int.Parse(exp.Item3) < MinInput || int.Parse(exp.Item3) > MaxInput);
        }
    }
}
