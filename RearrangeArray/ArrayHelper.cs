using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RearrangeArray
{
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
