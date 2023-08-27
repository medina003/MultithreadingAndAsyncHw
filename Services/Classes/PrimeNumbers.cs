using System;
using System.Collections.ObjectModel;
using System.Threading;
namespace MultithreadingAndAsync.Services.Classes;
public static class PrimeNumbers
{
    public static CancellationTokenSource Cts = new();
    public static CancellationToken Token = Cts.Token;
    public static ManualResetEvent PauseEvent = new(true);
    public static bool IsPaused { get; set; } = false;
    public static void PrintPrimeNumbers(int min, int max, ObservableCollection<int> PrimeNums)
    {
        int i = min, count = 0;
        if (max == 0)
        {
            while (!Token.IsCancellationRequested)
            {
                App.Current.Dispatcher.Invoke((Action)delegate 
                {
                    for (int j = 1; j < i + 1; j++) if (i % j == 0) count++;
                    if (count == 2) PrimeNums.Add(i);
                    count = 0;
                });
                Thread.Sleep(500);
                i++;
            }
        }
        else
        {
            if (min == 0) min = 2;
            for (i = min; i <= max; i++)
            {
                PauseEvent.WaitOne(); 
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    for (int j = 1; j < i + 1; j++) if (i % j == 0) count++;
                    if (count == 2) PrimeNums.Add(i);
                    count = 0;
                });
                Thread.Sleep(500);
            }
        }
    }
}  