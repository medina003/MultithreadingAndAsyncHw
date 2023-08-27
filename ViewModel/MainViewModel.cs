using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using MultithreadingAndAsync.Services.Classes;
namespace MultithreadingAndAsync.ViewModel;
public class MainViewModel : ViewModelBase
{
    public int MinPrime { get; set; }
    public int MaxPrime { get; set; }
    public ObservableCollection<int> PrimeNumsList { get; set; } = new();
    private static Thread? _primeThread;
    public RelayCommand PrimeNumsStartContinueCommand
    {
        get => new(() =>
        { 
            if (PrimeNumbers.IsPaused)
            {
                PrimeNumbers.PauseEvent.Set();
                PrimeNumbers.IsPaused = false;
            }
            else
            {
                _primeThread = new Thread(()=>PrimeNumbers.PrintPrimeNumbers(MinPrime, MaxPrime,PrimeNumsList) );
                _primeThread.Start();
            }
        });
    }
    public RelayCommand PrimeNumsPause
    {
        get => new(() =>
        {
            PrimeNumbers.PauseEvent.Reset();
            PrimeNumbers.IsPaused = true;
            if (MaxPrime == 0) PrimeNumbers.Cts.Cancel();
            MessageBox.Show("Paused");
        });
    }
    public RelayCommand PrimeNumsClear
    {
        get => new(() =>
        {
            PrimeNumsList.Clear();
            PrimeNumbers.IsPaused = true;
            PrimeNumbers.PauseEvent = new(true);
            MinPrime = MaxPrime = 0;
            MessageBox.Show("Cleaned");
        });
    }
    public int FibonachyNumber { get; set; }
    public ObservableCollection<int> FibonachyNumsList { get; set; } = new();
    private static Thread? _fibonachyThread;
    public RelayCommand FibonachyNumsStartContinueCommand
    {
        get => new(() =>
        {
            if (FibonachyNumbers.IsPaused)
            {
                FibonachyNumbers.PauseEvent.Set();
                FibonachyNumbers.IsPaused = false;
            }
            else
            {
                _fibonachyThread = new Thread(() => FibonachyNumbers.PrintFibonachyNumbers(FibonachyNumber, FibonachyNumsList));
                _fibonachyThread.Start();
            }
        });
    }
    public RelayCommand FibonachyNumsPause
    {
        get => new(() =>
        {
            FibonachyNumbers.PauseEvent.Reset();
            FibonachyNumbers.IsPaused = true;
            if (FibonachyNumber == 0) FibonachyNumbers.Cts.Cancel();
            MessageBox.Show("Paused");
        });
    }
    public RelayCommand FibonachyNumsClear
    {
        get => new(() =>
        {
            FibonachyNumsList.Clear();
            FibonachyNumbers.IsPaused = true;
            FibonachyNumbers.PauseEvent = new(true);
            FibonachyNumber = 0;
            MessageBox.Show("Cleaned");
        });
    }
}
