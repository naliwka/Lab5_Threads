using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab_5_2
{
    internal class Program
    {
        private static object monitor = new object();
        private static Semaphore semaphore = new Semaphore(2, 2);

        static void Main(string[] args)
        {
            Thread trafficLight1Thread = new Thread(TrafficLight1);
            Thread trafficLight2Thread = new Thread(TrafficLight2);
            Thread trafficLight3Thread = new Thread(TrafficLight3);
            Thread trafficLight4Thread = new Thread(TrafficLight4);

            trafficLight1Thread.Start();
            trafficLight2Thread.Start();
            trafficLight3Thread.Start();
            trafficLight4Thread.Start();

            Console.ReadLine();
        }
        static void TrafficLight1()
        {
            while (true)
            {
                // Зелене світло для TrafficLight1
                lock (monitor)
                {
                    Console.WriteLine("TrafficLight1: Зелене світло");

                    // Автомобілі проїжджають через перехрестя
                    semaphore.WaitOne();
                    Console.WriteLine("TrafficLight1: Автомобіль проїздить");
                    Thread.Sleep(1000);
                    semaphore.Release();
                }
                Console.WriteLine("TrafficLight1: Червоне світло");
                Thread.Sleep(2000);
            }
        }
        static void TrafficLight2()
        {
            while (true)
            {
                // Зелене світло для TrafficLight2
                lock (monitor)
                {
                    Console.WriteLine("TrafficLight2: Зелене світло");

                    // Автомобілі проїжджають через перехрестя
                    semaphore.WaitOne();
                    Console.WriteLine("TrafficLight2: Автомобіль проїздить");
                    Thread.Sleep(1000);
                    semaphore.Release();
                }
                Console.WriteLine("TrafficLight2: Червоне світло");
                Thread.Sleep(2000);
            }
        }
        static void TrafficLight3()
        {
            while (true)
            {
                // Зелене світло для TrafficLight3
                lock (monitor)
                {
                    Console.WriteLine("TrafficLight3: Зелене світло");

                    // Автомобілі проїжджають через перехрестя
                    semaphore.WaitOne();
                    Console.WriteLine("TrafficLight3: Автомобіль проїздить");
                    Thread.Sleep(1000);
                    semaphore.Release();
                }
                Console.WriteLine("TrafficLight3: Червоне світло");
                Thread.Sleep(2000);
            }
        }
        static void TrafficLight4()
        {
            while (true)
            {
                // Зелене світло для TrafficLight4
                lock (monitor)
                {
                    Console.WriteLine("TrafficLight4: Зелене світло");
                    semaphore.WaitOne();
                    Console.WriteLine("TrafficLight4: Автомобіль проїздить");
                    Thread.Sleep(1000);
                    semaphore.Release();
                }
                Console.WriteLine("TrafficLight4: Червоне світло");
                Thread.Sleep(2000);
            }
        }
    }
}
