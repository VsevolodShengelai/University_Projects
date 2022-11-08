using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read a line of text from the keyboard
            string line;

            //Loop until no more input(Ctrl - Z in a console, or end - of - file)
            while ((line = Console.ReadLine()) != null)
            {
                // Format the data
                line = line.Replace(",", " y:");
                line = "x:" + line;

                // Write the results out to the console window
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }
    }
}
