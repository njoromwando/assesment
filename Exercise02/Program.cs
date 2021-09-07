using System;
using Exercise01;

namespace Exercise02
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("Please Enter Number to Convert");
                string pText = Console.ReadLine();
                if (!string.IsNullOrEmpty(pText))
                {
                    if (pText.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                    {
                        exit = true;
                    }
                    else
                    {
                        var result = Convert.ToInt64(pText.Replace(",", string.Empty)).NumbersToWord();

                        Console.WriteLine(result);
                    }
                }
            }
            while (!exit);
        }
    }
}