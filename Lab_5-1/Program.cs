using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab_5_1
{
    internal class Program
    {
        private static Queue<int> queue = new Queue<int>();
        private static object lockObject = new object();

        static void Main(string[] args)
        {
            Thread producerThread = new Thread(Producer);
            Thread consumerThread = new Thread(Consumer);
            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();

            Console.WriteLine("Робота програми завершена");
            Console.ReadLine();
        }
        static void Producer()
        {
            Random random = new Random();

            while (true)
            {
                int number = random.Next(100);

                lock (lockObject)
                {
                    queue.Enqueue(number);
                    Console.WriteLine($"Виробник додав число: {number}");
                }
                Thread.Sleep(random.Next(1000, 2000));
            }
        }
        static void Consumer()
        {
            while (true)
            {
                int number;

                lock (lockObject)
                {
                    if (queue.Count > 0)
                    {
                        number = queue.Dequeue();
                        Console.WriteLine($"Споживач отримав число: {number}");
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
