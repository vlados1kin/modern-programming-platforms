using System.Collections.Concurrent;

namespace SPP.LaboratoryWork3;

public class LogBuffer : IDisposable
{
    private readonly StreamWriter _streamWriter;
    private readonly Timer _timer;
    private readonly ConcurrentQueue<string> _queue;
    private readonly object _flushLock = new();
    private int _currentSize;
    private readonly int _bufferSize;
    
    public LogBuffer(string path, int bufferSize, int timeout)
    {
        var fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize, FileOptions.Asynchronous);
        _bufferSize = bufferSize;
        _streamWriter = new StreamWriter(fileStream);
        _timer = new Timer(_ => SafeFlush(), null, 0, timeout);
        _queue = new ConcurrentQueue<string>();
    }

    public void LogMessage(string message)
    {
        _queue.Enqueue(message);
        if (Interlocked.Increment(ref _currentSize) >= _bufferSize)
        {
            SafeFlush();
        }
    }

    private void SafeFlush()
    {
        lock (_flushLock)
        {
            _ = FlushToFile();
        }
    }
    
    private async Task FlushToFile()
    {
        while (!_queue.IsEmpty)
        {
            if (_queue.TryDequeue(out var message))
            {
                await _streamWriter.WriteLineAsync(message);
                await _streamWriter.FlushAsync();
                Interlocked.Decrement(ref _currentSize);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The message \"{message}\" has been flushed to file.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }

    public void Dispose()
    {
        SafeFlush();
        FlushToFile().GetAwaiter().GetResult();
        _streamWriter.Dispose();
        _timer.Dispose();
    }
}