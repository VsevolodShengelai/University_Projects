using System;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;

            while ((line = Console.ReadLine()) != null)
            {
                Console.WriteLine("x:" + line.Replace(",", " y:"));
            }
            Console.Read();
        }
    }
}
