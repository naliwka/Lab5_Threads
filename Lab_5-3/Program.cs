using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab_5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 5, 2, 9, 1, 7, 6, 3, 8, 4 };

            Console.WriteLine("Original array:");
            PrintArray(arr);

            QuickSortParallel(arr);

            Console.WriteLine("Sorted array:");
            PrintArray(arr);

            Console.ReadLine();
        }
        static void QuickSortParallel(int[] arr)
        {
            QuickSortParallel(arr, 0, arr.Length - 1);
        }
        static void QuickSortParallel(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right);

                // Рекурсивний виклик QuickSortParallel для лівої та правої частини масиву в окремих потоках
                Thread leftThread = new Thread(() => QuickSortParallel(arr, left, pivotIndex - 1));
                Thread rightThread = new Thread(() => QuickSortParallel(arr, pivotIndex + 1, right));

                leftThread.Start();
                rightThread.Start();

                leftThread.Join();
                rightThread.Join();
            }
        }
        static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, right);

            return i + 1;
        }
        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        static void PrintArray(int[] arr)
        {
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }
}
