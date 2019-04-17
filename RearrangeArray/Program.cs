using System;
using System.Collections.Generic;
using System.Linq;

namespace RearrangeArray
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> someList = new List<string>()
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"

            };
                Console.WriteLine("Initial list: " + string.Join(", ", someList.ToArray()));
                someList = someList.Rearrange(someList.IndexOf("January"));
                Console.WriteLine("Result list 1: " + string.Join(", ", someList));
                someList = someList.Rearrange(someList.IndexOf("December"));
                Console.WriteLine("Result list 2: " + string.Join(", ", someList));
                someList = someList.Rearrange(someList.IndexOf("May"));
                Console.WriteLine("Result list 3: " + string.Join(", ", someList));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }

    public static class ArrayHelper
    {
        public static List<T> Rearrange<T>(this List<T> array, int divisionPoint)
        {
            if (divisionPoint < 0)
                throw new Exception("incorrect index supplied");
            if (divisionPoint == 0)
                return array;
            var result = array.Skip(divisionPoint).Take(array.Count() - divisionPoint).ToList();
            result.AddRange(array.Take(divisionPoint));
            return result;
        }
    }
}

