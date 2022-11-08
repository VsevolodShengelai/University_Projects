using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Yavno <inType> (dynamic from)
        {
            try
            {
                inType to = (inType)(dynamic)from;
            }
            catch
            {
                Console.WriteLine("ukhjk");
            }
        }

        static void Main(string[] args)
        {
            int from = 1;
            //string to = (string)(dynamic)from;
            Yavno <String> (from);
        }
    }
}
