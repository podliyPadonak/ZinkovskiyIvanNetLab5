using System;
using System.Diagnostics.Metrics;
using System.Threading;

namespace ZinkovskiyTask;

internal delegate double Formula(double i);
class Program
{
    static void Main(string[] args)
    {
        Formula formula0 = new Formula(Variant0Formula);
        Formula formula1 = new Formula(Variant1Formula);
        Formula formula2 = new Formula(Variant2Formula);
        Formula formula3 = new Formula(Variant3Formula);
        Formula formula4 = new Formula(Variant4Formula);
        Formula formula5 = new Formula(Variant5Formula);

        Console.WriteLine("0 Варіант:");
        CalcWithThreadFromTo(1, 12, formula0);
        Console.WriteLine("1 Варіант:");
        CalcWithThreadFromTo(1, 12, formula1);
        Console.WriteLine("2 Варіант:");
        CalcWithThreadFromTo(1, 12, formula2);
        Console.WriteLine("3 Варіант:");
        CalcWithThreadFromTo(1, 12, formula3);
        Console.WriteLine("4 Варіант:");
        CalcWithThreadFromTo(1, 12, formula4);
        Console.WriteLine("5 Варіант:");
        CalcWithThreadFromTo(1, 12, formula5);

        Console.ReadKey();
    }

    static void CalcWithThreadFromTo(int start, int end, Formula f)
    {
        for (int threads = start; threads <= end; threads++)
        {
            DateTime timeStart = DateTime.Now;
            SeriesCalculator c = new SeriesCalculator(f);
            double response = c.Calculate(threads);
            DateTime timeEnd = DateTime.Now;
            TimeSpan timeTotal = timeEnd - timeStart;
            Console.WriteLine("Sum of series: {0}\tThreads: {1}\ttime: {2} seconds", response, threads, timeTotal.TotalSeconds);
        }
    }

    static internal double Variant0Formula(double i)
    {
        return 1.0 / (1.0 + i * i * i);
    }
    static internal double Variant1Formula(double i)
    {
        return i / (1.0 + i * i * i * i);
    }
    static internal double Variant2Formula(double i)
    {
        return i * i / (1.0 + i * i * i * i);
    }
    static internal double Variant3Formula(double i)
    {
        double resp = 5.0 / ((i * i) * (1 + i * i));
        return resp;
    }
    static internal double Variant4Formula(double i)
    {
        double ch = Math.Pow(i, (2F/3F));
        double zn = Math.Pow((1 + i * i * i * i), (1F / 2F));
        return ch / zn;
    }
    static internal double Variant5Formula(double i)
    {
        return i / i;
    }
}