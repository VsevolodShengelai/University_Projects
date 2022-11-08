using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void PrintName()
        {
            Console.WriteLine($"Меня зовут {FirstName}!");

        }
    }

    class Student : Person
    {

    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Print()
        {
            Console.WriteLine("X:\t" + X);
            Console.WriteLine("Y:\t" + Y);
        }
    }

    //Класс Counter: счётчик-секундомер, который хранит кол-восекунд в свойстве Seconds
    //Первый метод преобразует число - объект типа int к типу Counter. Создается новый объект Counter, у которого устанавливается свойство Seconds.
    //Второй метод преобразует объект Counter к типу int, то есть получает из Counter число.

    class Counter
    {
        public int Seconds { get; set; }

        public static implicit operator Counter(int x)
        {
            return new Counter { Seconds = x };
        }
        public static explicit operator int(Counter counter)
        {
            return counter.Seconds;
        }
    }
}
