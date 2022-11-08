using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(NOD(16, 24, 36, 48, 60));
            Console.ReadLine();
        }

        public static int NOD(int a, int b) //Метод Евклида
        {
            if (a == 0) return b;
            while (b != 0)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }
            return a;
        }

        // Перегружаем метод
        // Он работает!!! Вся логика записана на бумаге
        public static int NOD(params int[] list)
        {

            int temp = list[list.Length - 1];
            int []new_list = new int [list.Length - 1];

            for (int i = 0; i < new_list.Length; i++)
                new_list[i] = list[i];

            int back = 0;

            if (new_list.Length == 2)
            {
                back = NOD(new_list[0], new_list[1]);
                return back;
            }

            back = NOD(new_list);
            temp = NOD(back, temp);



            return temp;
        }
    }
}
