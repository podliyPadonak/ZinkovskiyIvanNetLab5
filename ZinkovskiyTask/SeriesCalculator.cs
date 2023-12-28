using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZinkovskiyTask;

internal class SeriesCalculator
{
    private readonly Mutex mutex = new Mutex(); // М’ютекс
    private double sum = 0; // Підсумкова сума
    private readonly Formula formula;

    internal SeriesCalculator(Formula f)
    {
        this.formula = f;
    }

    internal double Calculate(int numThreads)
    {
        int maxIter = 100000000; // Загальна кількість ітерацій
        int step = maxIter / numThreads; // Кількість ітерацій у потоці
        Thread[] threads = new Thread[numThreads];
        // Запуск потоків
        for (int i = 0; i < numThreads; i++)
        {
            int begin = i * step;
            int end = (i == numThreads - 1) ? maxIter : (i + 1) * step;

            // Створення і запуск потоку
            threads[i] = new Thread(() => CalcSeries(begin, end));
            threads[i].Start();
        }
        // Очікування завершення потоків
        foreach (var thread in threads)
        {
            thread.Join();
        }
        return sum;
    }

    private void CalcSeries(int begin, int end)
    {
        double localSum = 0;
        for (double i = begin; i < end; i++)
        {
            localSum += formula(i);
        }
        mutex.WaitOne();
        sum += localSum;
        mutex.ReleaseMutex();
    }
}



