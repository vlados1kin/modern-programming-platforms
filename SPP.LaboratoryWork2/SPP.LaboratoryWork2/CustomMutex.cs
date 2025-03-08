namespace SPP.LaboratoryWork2;

public class CustomMutex
{
    private int _currentId = -1;

    public void Lock()
    {
        while (Interlocked.CompareExchange(ref _currentId, Thread.CurrentThread.ManagedThreadId, -1) != -1)
        {
            Thread.Yield();
        }
    }

    public void Unlock()
    {
        Interlocked.CompareExchange(ref _currentId, -1, Thread.CurrentThread.ManagedThreadId);
    }
}