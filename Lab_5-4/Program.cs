using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab_5_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrixA = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

            int[,] matrixB = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
            int[,] resultMatrix = MultiplyMatricesParallel(matrixA, matrixB);

            Console.WriteLine("Result Matrix:");
            PrintMatrix(resultMatrix);

            Console.ReadLine();
        }

        static int[,] MultiplyMatricesParallel(int[,] matrixA, int[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int colsB = matrixB.GetLength(1);

            int[,] resultMatrix = new int[rowsA, colsB];

            SemaphoreSlim semaphore = new SemaphoreSlim(Environment.ProcessorCount);
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    semaphore.Wait(); // Заблокувати доступ до обчислення для інших потоків
                    ThreadPool.QueueUserWorkItem(state =>
                    {
                        int row = (int)((object[])state)[0];
                        int col = (int)((object[])state)[1];

                        resultMatrix[row, col] = CalculateCell(matrixA, matrixB, row, col);

                        semaphore.Release(); // Звільнити семафор для інших потоків

                    }, new object[] { i, j });
                }
            }
            semaphore.Wait(); // Заблокувати головний потік, щоб всі потоки завершили роботу
            semaphore.Release(); // Звільнити семафор для майбутніх викликів

            return resultMatrix;
        }
        static int CalculateCell(int[,] matrixA, int[,] matrixB, int row, int col)
        {
            int sum = 0;
            int colsA = matrixA.GetLength(1);

            for (int k = 0; k < colsA; k++)
            {
                sum += matrixA[row, k] * matrixB[k, col];
            }

            return sum;
        }
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
