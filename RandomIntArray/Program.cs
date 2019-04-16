using System;
using System.Diagnostics;
using System.Linq;
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
            if (int.TryParse(enteredSequence, out int maxValue) && maxValue <= int.MaxValue)
            {
                var arrayBuilder = new ArrayBuilder();
                var result = arrayBuilder.BuildRandomIntArray(maxValue);
                Console.WriteLine(String.Join(", ", result.Item1));
                Console.WriteLine(new string('=', 50));
                Console.WriteLine(new string(' ', 50));
                Console.WriteLine("Execution time: " + result.Item2.ToString());
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
        public Tuple<int[], TimeSpan> BuildRandomIntArray(int maxValue)
        {
            int[] range = Enumerable.Range(1, maxValue).ToArray();
            TimeSpan runTime = Shuffle(range);
            return new Tuple<int[], TimeSpan>(range, runTime);
        }
       
        private TimeSpan Shuffle(int[] items)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Random rand = new Random();
            var degreeOfParallelism = Environment.ProcessorCount;

            var tasks = new Task[degreeOfParallelism];

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                int taskNumberCopy = taskNumber;

                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        var max = items.Length * (taskNumberCopy + 1) / degreeOfParallelism;
                        for (int i = items.Length * taskNumberCopy / degreeOfParallelism;
                            i < max;
                            i++)
                        {
                            int j = rand.Next(i, items.Length);
                            int temp = items[i];
                            items[i] = items[j];
                            items[j] = temp;
                        }
                    });
            }

            Task.WaitAll(tasks);
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
