using System;

namespace training
{
    class Program
    {
        class A { }
        class B : A { }
        class C : A { }

        class MyClass
        {
            int value;
            public MyClass(bool b = false)
            {
                this.value = b ? 1 : 0;
            }
            public static implicit operator bool(MyClass d) => d.value == 1 ? true : false;
            public static explicit operator MyClass(bool b) => new MyClass(b);
            public override string ToString() => $"{value}";
        }
        static void Main(string[] args)
        {
            /*
             * Пункт 1)
             * Можно неявное преобразование из меньшего типа в больший, кроме bool и decimal
             * Нельзя неявно конвертировать из u-тип в знаковый тип и обратно.
             * short -> int -> long -> float -> double
             * char -> ushort -> uint -> long
             * Неявное преобразование можно сделать из дочернего класса в базовый.
             */
            bool b = true;
            decimal dec = 1.0m;

            short sh = 1;
            int i = 1;
            long l = 1;
            float f = 1.0f;
            double d = 1.0;

            d = f = l = i = sh;

            char c = 'a';
            ushort ush = 1;
            uint ui = 1;
            ulong ul = 1;

            // преобразование
            ul = ui = ush = c;

            B obj_b = new B();
            A obj_a = obj_b;
            

            /*
             * Пункт 2)
             * Явное преобразование можно из любого примитивного типа в другой примитивыный, кроме bool
             * Явное преобразование можно из базового в дочерний класс. Существует ряд проверок.
             */
            ush = (ushort) d;
            i = (int) c;
            i = (int) dec;

            obj_b = (B)obj_a; // опасно!

            // Пункт 3)
            try
            {
                Convert.ToChar(b);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Ошибка преобразования: " + e.ToString());
            }

            /*
             * Пункт 4)
             * Безопасное приведение типов
             */

            if (obj_a.GetType() == typeof(B)) { /*...*/ }

            if (obj_a is B)
                obj_b = (B)obj_a;

            obj_b = obj_a as B;
            if (obj_b == null) { /*...*/ }

            /*
             * Пункт 5)
             */

            MyClass my = new();
            my = (MyClass) false; // явное
            /* bool */ b = my; // неявное

            /*
             * Пункт 6)
             * Можно преобразовать из любого примитивного в другой примитивный.
             * Некоторые методы вызывают исключение.
             */
            i = Convert.ToInt32(d);
            c = Convert.ToChar(ui);
            Convert.ToString(true);

            string str_num = "12345.1";
            if (Int32.TryParse(str_num, out i))
            {
                Console.WriteLine($"Удалось преобразовать {str_num} в int: {i}");
            }
            else
            {
                Console.WriteLine($"Не удалось преобразовать {str_num} в int");
            }

            try
            {
                Console.WriteLine("Int32.Parse(\"test\") выдаёт " + Int32.Parse("test"));
            }
            catch (FormatException e)
            {
                Console.WriteLine("Int32.Parse(\"test\") выдаёт исключение");
            }
        }
    }
}
