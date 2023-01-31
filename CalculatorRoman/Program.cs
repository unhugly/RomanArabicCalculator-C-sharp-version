using System;

namespace CalculatorRoman
{
    class Program
    {
        static void Main(string[] args) {
            Calculator kataCalc = new Calculator();
            while (true) {
                try {
                    Console.Write(">");
                    Console.WriteLine($"Result: {kataCalc.NextExpression(Console.ReadLine())}");
                }
                catch (Exception e) {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
    }
}
