using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Fifteen
{
    public class Game
    {
        public enum MoveDirection { Up, Down, Left, Right}

        Random rnd = new Random();
        public int[,] map = new int[4, 4];
        int step;

        public event EventHandler<int[,]> RePaint;

        public int Step => step;

        public void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    map[i, j] = (i * 4 + j + 1) % 16;
                }
            }
            Mix();
            step = 0;
            RePaint?.Invoke(this, map);
        }

        public void KeyDown(MoveDirection key)
        {
            switch (key)
            {
                case MoveDirection.Left: ToLeft() ; break;
                case MoveDirection.Right: ToRight(); break;
                case MoveDirection.Up: ToUp(); break;
                case MoveDirection.Down: ToDown(); break;
            }
            RePaint?.Invoke(this, map);
        }

        void Mix()
        {
            for (int i = 0; i < 200; i++)
            {
                switch (rnd.Next(2) + i%2*2)
                {
                    case 0: ToLeft(); break;
                    case 1: ToRight(); break;
                    case 2: ToUp(); break;
                    case 3: ToDown(); break;
                }
            }
        }

        public bool Win()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] != i * 4 + j + 1 % 16)
                        return false;
                }
            }
            return true;
        }

        (int, int) FindSpace()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == 0)
                        return (i, j);
                }
            }
            throw new ArgumentException("Нуля не обнаружено");
        }

        static void swap(ref int a, ref int b) => (a, b) = (b, a);

        void ToDown()
        {
            var (r, c) = FindSpace();
            if (r > 0)
            {
                swap(ref map[r - 1, c], ref map[r, c]);
                step++;
            }
        }

        void ToUp()
        {
            var (r, c) = FindSpace();
            if (r < 3)
            {
                swap(ref map[r, c], ref map[r + 1, c]);
                step++;
            }
        }

        void ToRight()
        {
            var (r, c) = FindSpace();
            if (c > 0)
            {
                swap(ref map[r, c], ref map[r, c - 1]);
                step++;
            }
        }

        void ToLeft()
        {
            var (r, c) = FindSpace();
            if (c<3)
            {
                swap(ref map[r, c], ref map[r, c + 1]);
                step++;
            }
        }
    }
}
