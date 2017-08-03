using System;
using static System.Console;

namespace InstaMass
{
    static class DisplayHelper
    {
        public static void WriteLine(string message)
        {
            var originalColor = ForegroundColor;
                        
            ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);

            ForegroundColor = originalColor;
        }
    }
}
