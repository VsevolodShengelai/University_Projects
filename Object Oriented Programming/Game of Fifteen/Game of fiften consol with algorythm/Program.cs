 using System;
using System.Threading.Tasks;
using System.Timers;

namespace Game_of_Fifteen_console
{
    class Program
    {
        static Timer timer;
        static bool flag = true;
        Game game;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            var game = new Game();

            game.RePaint += Print ;
            game.Init();

            do //Игровой цикл
            {
                game.KeyDown(Console.ReadKey().Key);

                //После сдвига перерисовываем консоль 
            }
            while (!game.Win());
            timer.Stop();
            Console.WriteLine("You Win!!!");
            Console.ReadKey();
        }

        void Print(int[,] map)
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
                        Console.Write($"{map[i, j],3}"); // Ширина 3 выр. по левому краю
                }
                Console.WriteLine();
            }
            flag = true;
        }

        static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (flag)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("GameTime:" + (int)(DateTime.Now - start).TotalSeconds);
            }
        }



    }
}