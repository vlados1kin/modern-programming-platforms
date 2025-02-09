using System.Collections.Concurrent;

namespace SPP.LaboratoryWork1.TaskQueue;

public class TaskQueue
{
    public delegate void TaskDelegate();

    private readonly ConcurrentQueue<TaskDelegate> _queue;
    private readonly Thread[] _threads;
    private volatile bool _isStopping = false;

    public TaskQueue(int count)
    {
        _queue = new ConcurrentQueue<TaskDelegate>();
        _threads = new Thread[count];
        for (var i = 0; i < count; i++)
        {
            _threads[i] = new Thread(ThreadProcess) { IsBackground = false };
            _threads[i].Start();
        }
    }

    public void NewTask(TaskDelegate task)
    {
        _queue.Enqueue(task);
    }

    public void WaitAll()
    {
        _isStopping = true;
        
        foreach (var thread in _threads)
        {
            thread.Join();
        }
    }

    private void ThreadProcess()
    {
        while (!_isStopping || !_queue.IsEmpty)
        {
            if (_queue.TryDequeue(out var task))
            {
                try
                {
                    task.Invoke();
                }
                catch (ThreadStateException exception)
                {
                    Console.WriteLine($"ThreadStateException: {exception.Message}");
                }
                catch (ThreadAbortException exception)
                {
                    Console.WriteLine($"ThreadAbortException: {exception.Message}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception: {exception.Message}");
                }
            }
        }
    }
}