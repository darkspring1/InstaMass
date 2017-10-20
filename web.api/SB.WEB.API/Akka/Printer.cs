using Akka.Actor;
using System;

namespace SM.WEB.API
{
    /// <summary>
    /// Prints recommendations out to the console
    /// </summary>
    public class Printer : ReceiveActor
    {
        public Printer()
        {
            Receive<String>(res =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Environment.NewLine}{res}");
                Console.ResetColor();
            });
        }
    }
}
