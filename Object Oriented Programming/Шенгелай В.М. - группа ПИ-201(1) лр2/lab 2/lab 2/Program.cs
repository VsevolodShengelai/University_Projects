using System;
using System.Globalization;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //неявные преобразования
            /*
             * byte -> short, ushort, int, uint, long, ulong, float, double, decimal
             * sbyte -> short, int, long, float, double, decimal
             * short -> int, long, float, double, decimal
             * ushort -> int, uint, long, ulong, float, double, decimal
             * int -> long, float, double, decimal
             * uint -> long, ulong, float, double, decimal
             * long -> float, double, decimal
             * ulong -> float, double, decimal
             * float -> double
             * char -> ushort, int, uint, long, ulong, float, double, decimal
             */

            // Не поддерживается неявное преобразование в тип char
            // Неявные преобразования между типами с плавающей запятой и типом decimal отсутствуют

            //Неявное преобразование числового типа
            int num = 2147483647;
            long bigNum = num;

            //Неявное преобразование ссылочного типа
            Student d = new Student();
            Person b = d;


            // явные преобразования
            /*
              sbyte   -> byte, ushort, uint или ulong либо 
              byte    -> short, sbyte, byte, ushort, uint, ulong или nuint
              ushort  -> sbyte, byte или short
              int     -> sbyte, byte, short, ushort, uint, ulong или nuint
              uint    -> sbyte, byte, short, ushort или int
              long    -> sbyte, byte, short, ushort, int, uint, ulong, nint или nuint
              ulong   -> sbyte, byte, short, ushort, int, uint, long, nint или nuint
              float   -> sbyte, byte, short, ushort, int, uint, long, ulong, decimal, nint или nuint
              double  -> sbyte, byte, short, ushort, int, uint, long, ulong, float, decimal, nint или nuint
              decimal -> sbyte, byte, short, ushort, int, uint, long, ulong, float, double, nint или nuint
              nint    -> sbyte, byte, short, ushort, int, uint, ulong или nuint
              nuint   -> sbyte, byte, short, ushort, int, uint, long или nint
            */

            double x = 1234.7;
            int a = (int) x;
            Console.WriteLine("double x = 1234.7 явно в int: " + a);

            // Неявное преобразование в базовый тип бзеопасно
            Student g = new Student();
            Person s = g;

            // Явное преобразование требуется для возврата к производному типу.
            // Примечание: это будет компилироваться, но вызовет исключение во время выполнения,
            // если правосторонний объект на самом деле не является Студентом.
            Student g2 = (Student)s;

            //Обработка исключений
            //Микропрограмма ввода числа и вывода его квадрата
            try
            {
                Console.WriteLine("\nПодпрограмма возведения числа в квадрат");
                Console.WriteLine("Введите число");
                int number = Int32.Parse(Console.ReadLine());
                number *= number;
                Console.WriteLine("Квадрат числа: " + number);
            }

            catch
            {
                Console.Write("Возникло исключение!\n");
            }
            finally
            {
                Console.Write("Блок finally. Работа подпрограммы завершена.\n\n");
            }

            // is и as
            static void SafeFunction_as (object obj)
            {
                Point point = obj as Point;

                if (point != null)
                {
                    Console.WriteLine("Объект оказался Point");
                    point.Print();
                }
                else
                {
                    Console.WriteLine("Объект не Point! Его приведение не было успешным!");
                }
            }

            static void SafeFunction_is (object obj)
            {
                if (obj is Point)
                {
                    Point point = (Point)obj;
                    Console.WriteLine("Объект оказался Point");
                    point.Print(); 
                }
                else
                {
                    Console.WriteLine("Объект не Point! Его приведение не было успешным!");
                }
            }

            object obj = new Point { X = 3, Y = 5 };
            SafeFunction_as(obj);

            Console.WriteLine("");

            obj = "Some String";
            SafeFunction_as(obj);

            //Пользовательское преобразование типов Implicit, Explicit;
            Counter counter1 = new Counter { Seconds = 23 };

            int y = (int)counter1; // Явное
            Console.WriteLine(x);

            Counter counter2 = y; // Неявное
            Console.WriteLine(counter2.Seconds);

            // Подпрограмма сложения двух чисел, введённых через консоль
            string str = "5";
            int number1, number2;

            Console.WriteLine("\nПодпрограмма сложения двух чисел");
            Console.WriteLine("Введите число 1");
            str = Console.ReadLine();
            number1 = Convert.ToInt32(str);

            Console.WriteLine("Введите число 2");
            str = Console.ReadLine();
            number2 = Convert.ToInt32(str);

            Console.WriteLine("Сумма чисел = " + (number1 + number2));
            Console.WriteLine("Работа подпрограммы завершена.");

            //Методы Parse/ TryParse

            str = "5";

            NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ".",
            };

            int temp_1 = int.Parse(str);
            double temp_2 = double.Parse(str, numberFormatInfo);

            bool result = int.TryParse(str, out temp_1);
            if (result)
            {
                Console.WriteLine("Операция успешна, значение = " + temp_1);
            }
            else
            {
                Console.WriteLine("не удалось конвертировать!");
            }
        }
    }
}
