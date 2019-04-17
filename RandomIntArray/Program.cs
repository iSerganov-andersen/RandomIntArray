using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RandomIntArray
{
    class Program
    {
        static void Main(string[] args)
        {
        Start:
            Console.WriteLine("Please enter maximum array value (Integer)");
            var enteredSequence = Console.ReadLine();
            if (int.TryParse(enteredSequence, out int maxValue) && maxValue > 0 && maxValue <= int.MaxValue)
            {
                var arrayBuilder = new ArrayBuilder();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var result = arrayBuilder.BuildRandomIntArray(maxValue);
                sw.Stop();
                Console.WriteLine(new string('=', 50));
                Console.WriteLine(new string(' ', 50));
                Console.WriteLine("Execution time: " + sw.Elapsed.ToString());
                //Console.WriteLine("Has duplicates: " + arrayBuilder.CheckforDuplicates(result).ToString());
                Console.WriteLine(new string(' ', 50));
                Console.WriteLine(new string('=', 50));
                Console.WriteLine(new string(' ', 50));
                goto Start;

            }
            else
            {
                Console.WriteLine("You should wright a valid integer");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine(new string(' ', 50));
                goto Start;
            }
        }
    }

    public class ArrayBuilder
    {
        public int[] BuildRandomIntArray(int maxValue)
        {
            int[] range = Enumerable.Range(1, maxValue).ToArray();
            return Shuffle(range);
        }

        private int[] Shuffle(int[] items)
        {
            Random rand = new Random();
            var degreeOfParallelism = Environment.ProcessorCount;

            var tasks = new Task[degreeOfParallelism];
            var arrays = Split(items, items.Length / degreeOfParallelism).ToArray();

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                int taskNumberCopy = taskNumber;

                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        for (int i = 0; i < arrays[taskNumberCopy].Length; i++)
                        {
                            int j = rand.Next(i, arrays[taskNumberCopy].Length);
                            int temp = arrays[taskNumberCopy][i];
                            arrays[taskNumberCopy][i] = arrays[taskNumberCopy][j];
                            arrays[taskNumberCopy][j] = temp;
                        }
                    });
            }

            Task.WaitAll(tasks);
            var listItems = new List<int>();
            for (int i = 0; i < arrays.Length; i++)
            {
                listItems.AddRange(arrays[i]);
            }
            return listItems.ToArray();
        }

        public int[] CreateUniqueNumbersArray(int start, int end)
        {
            var random = new Random();
            //Creating a sorted array
            var array = Enumerable.Range(start, end).ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                //Obtaining a random index.
                var randomIndex = random.Next(i + 1);
                // Getting the numbers at the current position and the random position.
                var n1 = array[randomIndex];
                var n2 = array[i];
                // Swapping them.
                array[randomIndex] = n2;
                array[i] = n1;
            }
            return array;

        }

        public bool CheckforDuplicates(int[] array)
        {
            var duplicates = array
             .GroupBy(p => p)
             .Where(g => g.Count() > 1)
             .Select(g => g.Key);

            return (duplicates.Count() > 0);
        }

        public IEnumerable<int[]> Split(int[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size).ToArray();
            }
        }
    }
}
