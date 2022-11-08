using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace gcd_wpf
{
    class GCDAlgorithms
    {
        public static int EuclidGCD(int a, int b)
        {
            if (a == 0 || b == 0) return a == 0 ? b : a;
            while (b != 0)
                if (a > b)
                    a -= b;
                else
                    b -= a;
            return a;
        }
        public static int EuclidGCDVararg(params int[] list)
        {
            int result = list[0];
            foreach (int var in list.Skip(1))
            {
                result = EuclidGCD(result, var);
            }
            return result;
        }
        public static int EuclidGCD(int a, int b, int c) => EuclidGCDVararg(a, b, c);
        public static int EuclidGCD(int a, int b, int c, int d) => EuclidGCDVararg(a, b, c, d);
        public static int EuclidGCD(int a, int b, int c, int d, int e) => EuclidGCDVararg(a, b, c, d, e);
        public static int SteinGCD(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;
            if ((a & 1) == 0)
                if ((b & 1) == 0)
                    return 2 * SteinGCD(a >> 1, b >> 1);
                else return SteinGCD(a >> 1, b);
            if ((b & 1) == 0)
                return SteinGCD(a, b >> 1);
            if (a >= b)
                return SteinGCD((a - b) >> 1, b);
            return SteinGCD((b - a) >> 1, a);
        }
        public static int SteinGCDVararg(params int[] list)
        {
            int result = list[0];
            foreach (int var in list.Skip(1))
            {
                result = SteinGCD(result, var);
            }
            return result;
        }
        public static int SteinGCD(int a, int b, int c) => SteinGCDVararg(a, b, c);
        public static int SteinGCD(int a, int b, int c, int d) => SteinGCDVararg(a, b, c, d);
        public static int SteinGCD(int a, int b, int c, int d, int e) => SteinGCDVararg(a, b, c, d, e);

        public static void EuclidGCDTest()
        {
            Debug.Assert(EuclidGCD(2806, 345) == 23);
            Debug.Assert(EuclidGCD(7396, 1978, 1204) == 86);
            Debug.Assert(EuclidGCD(7396, 1978, 1204, 430) == 86);
            Debug.Assert(EuclidGCD(7396, 1978, 1204, 430, 258) == 86);
        }
        public static void SteinGCDTest()
        {
            Debug.Assert(SteinGCD(298467352, 569484) == 4);
            Debug.Assert(SteinGCD(7396, 1978, 1204) == 86);
            Debug.Assert(SteinGCD(7396, 1978, 1204, 430) == 86);
            Debug.Assert(SteinGCD(7396, 1978, 1204, 430, 258) == 86);
        }
        public static void DoTests()
        {
            EuclidGCDTest();
            SteinGCDTest();
        }
        public static void SpeedTest()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                EuclidGCD(298467352, 569484);
                SteinGCD(298467352, 569484);
            }
            Stopwatch sw = new();
            sw.Start();
            for (int i = 0; i < 1000000; ++i)
            {
                EuclidGCD(298467352, 569484);
            }
            sw.Stop();
            var euclidTime = sw.ElapsedTicks;
            Console.WriteLine($"Метод Эвклида занял {euclidTime} тиков");

            sw.Reset();
            sw.Start();
            for (int i = 0; i < 1000000; ++i)
            {
                SteinGCD(298467352, 569484);
            }
            sw.Stop();
            var steinTime = sw.ElapsedTicks;
            Console.WriteLine($"Метод Штейна занял {steinTime} тиков");

        }
    }
}
