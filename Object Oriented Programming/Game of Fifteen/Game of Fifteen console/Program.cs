using System;
using System.Threading.Tasks;
using System.Timers;

namespace Game_of_Fifteen_console
{
    class Program
    {
        static Random rnd = new Random();
        static int[,] map = new int[4, 4];
        static int step;
        static Timer timer;
        static DateTime start = DateTime.Now;
        static bool flag = true;

        static void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    map[i, j] = (i * 4 + j + 1) % 16; //с 1 до 16 //16-я ячейка обернётся в 0
                }
            }
            step = 0;
        }

        static async void Mix()
        {
            for (int i = 0; i < 200; i++)
            {
                switch (rnd.Next(4))
                {
                    case 0: 
                        ToLeft();
                        break;
                    case 1:
                        ToRight();
                        break;
                    case 2:
                        ToUp();
                        break;
                    case 3:
                        ToDown();
                        break;
                }
                /*
                if (i % 4 == 0)
                    await Task.Delay(100);
                Print();*/
            }
        }

        static bool Win()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] != (i * 4 + j + 1) % 16)
                        return false;
                }
            }
            return true;
        }
        static void Print()
        {
            flag = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Step: " + step + "\n");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == 0)
                        Console.Write("   ");
                    else
                        Console.Write($"{map[i, j], 3}"); // Ширина 3 выр. по левому краю
                }
                Console.WriteLine();
            }
            flag = true;
        }


        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (flag)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("GameTime:" + (int)(DateTime.Now - start).TotalSeconds);
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            /*
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            */
            Init();
            Mix();
            do //Игровой цикл
            {
                step += 1;
                Print();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow: ToLeft(); break;
                    case ConsoleKey.RightArrow: ToRight(); break;
                    case ConsoleKey.UpArrow: ToUp(); break;
                    case ConsoleKey.DownArrow: ToDown(); break;
                }
                //После сдвига перерисовываем консоль 
            }
            while (!Win());
            Print();
            timer.Stop(); 
            Console.WriteLine("You Win!!!");
            Console.ReadKey();
        }


        static (int, int) FindSpace() //Возвращаем кортеж - координаты точки
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++) 
                {
                    if (map[i, j] == 0)
                        return (i, j);
                }    
            }
            return (-1, -1);
            throw new ArgumentException("Не был найден нулевой элемент");
        }

        static void swap(ref int a, ref int b) => (a, b) = (b, a);//поменять фишки местами

        private static void ToDown() 
        {
            var (r, c) = FindSpace();
            if (r > 0) swap(ref map[r-1, c], ref map[r, c]);  // Если пустая фишка не в верхнем ряду
        }

        private static void ToUp()
        {
            var (r, c) = FindSpace();
            if (r < 3) swap(ref map[r, c], ref map[r + 1, c]);
        }

        private static void ToRight()
        {
            var (r, c) = FindSpace();
            if (c > 0) swap(ref map[r, c], ref map[r, c - 1]);
        }

        private static void ToLeft()
        {
            var (r, c) = FindSpace();
            if (c < 3) swap(ref map[r, c], ref map[r, c + 1]);
        }
    }
}