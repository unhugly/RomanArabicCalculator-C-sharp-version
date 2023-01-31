using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorRoman
{
    public static class LineHandler
    {
        public static IList<char> Operators { get; } = new List<char> { '+', '-', '*', '/' };
        public static IList<char> RomanSymbols { get; } = new List<char> { 'i', 'v', 'x' };
        public static IList<char> ArabicSymbols { get; } = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        public static Tuple<string, char, string> HandleExpression(string str) {
            string exp = ClearLineOfSpaces(str.ToLower());
            CheckValid(exp);
            return new Tuple<string, char, string>(FindLeftOperand(exp), DetermineOperator(exp), FindRightOperand(exp));
        }

        private static string FindLeftOperand(string input) {
            StringBuilder result = new StringBuilder(input);
            return result.Remove(FindOperatorIndex(result.ToString()), result.Length - FindOperatorIndex(result.ToString())).ToString();
        }

        private static string FindRightOperand(string input) {
            StringBuilder result = new StringBuilder(input);
            return result.Remove(0, FindOperatorIndex(result.ToString())+1).ToString();
        }

        private static char DetermineOperator(string str) {
            for (int i = 0; i < Operators.Count; ++i) 
                foreach (char c in str.ToCharArray()) 
                    if (c == Operators[i]) 
                        return Operators[i];
            throw new Exception("Operator was't found");
        }

        private static void CheckValid(string str) {
            Console.WriteLine("Validating input..");
            if (str is null) throw new Exception("Uninitialized input");
            if (str == "") throw new Exception("Empty line");

            StringBuilder resultOperator = new StringBuilder();
            for (int i = 0; i < str.ToCharArray().Length; ++i)
                foreach (char op in Operators)
                    if (op == str.ToCharArray()[i]) 
                        resultOperator.Append(str.ToCharArray()[i]);
            if (resultOperator.Length > 1) throw new Exception("More than 1 operator");
            if (resultOperator.Length < 1) throw new Exception("Operator wasn't found");

            for (int i = 0; i < str.Length; ++i)
                if (str.ToCharArray()[i] == resultOperator[0])
                    if (i == 0 || i == str.Length - 1)
                        throw new Exception("Operator must be between two operands");

            StringBuilder resultSymbols = new StringBuilder(str);
            resultSymbols.Remove(FindOperatorIndex(str), 1);

            foreach (char c in resultSymbols.ToString().ToCharArray())
                if (!RomanSymbols.Contains(c) && !ArabicSymbols.Contains(c)) 
                    throw new Exception("Operands wasn't found /or/ Illigal symbols");

            if (IsContains(resultSymbols.ToString(), ArabicSymbols) && IsContains(resultSymbols.ToString(), RomanSymbols)) 
                throw new Exception("It is not possible to use both numerals(actually does, but task had other conditions)");
            Console.WriteLine("Validate completed!");
        }

        private static string ClearLineOfSpaces(string str) {
            StringBuilder result = new StringBuilder();
            char[] arr = str.ToCharArray();
            for (int i = 0; i < arr.Length; ++i) {
                if (arr[i] == ' ') continue;
                result.Append(arr[i]);
            }
            return result.ToString();
        }

        private static int FindOperatorIndex(string input) {
            int operIndex = -1;
            for (int i = 0; i < input.ToCharArray().Length; ++i)
                foreach (char op in Operators)
                    if (input.ToCharArray()[i] == op)
                        operIndex = i;
            return operIndex;
        }

        private static bool IsContains(string str, IList<char> list){
            foreach (char c in str.ToCharArray())
                if (list.Contains(c)) return true;
            return false;
        }
    }
}
