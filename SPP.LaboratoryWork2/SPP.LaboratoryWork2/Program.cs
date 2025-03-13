using SPP.LaboratoryWork2.Os;

namespace SPP.LaboratoryWork2;

public class Program
{
    private static CustomMutex _mutex = new();
    private static int _sum = 0;
    
    public static void Main()
    {
        // Task1();
        Task2();
    }

    private static void DoWork(int i)
    {
        _mutex.Lock();
        _sum += i;
        _mutex.Unlock();
    }
    
    private static void Task1()
    {
        var threads = new Thread[5];
        for (var i = 0; i < 5; i++)
        {
            var i1 = i;
            threads[i] = new Thread(() => DoWork(i1));
            threads[i].Start();
        }
        for (var i = 0; i < 5; i++)
        {
            threads[i].Join();
        }
        Console.WriteLine(_sum);
    }

    private static void Task2()
    {
        // 0x80000000 - generic_read, 3 - open_existing
        using (var osHandle = FileHelper.CreateFileHandle(@"..\..\..\test.txt", 0x80000000, 3))
        {
            if (osHandle != null)
            {
                FileHelper.ReadContent(osHandle, message => Console.WriteLine(message));
            }
        }
    }
}