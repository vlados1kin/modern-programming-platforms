using System.Diagnostics;

namespace SPP.LaboratoryWork1;

public static class Program
{
    private static int _countCopiedFiles = 0;
    private static object _o = new();

    // The first arg is the source folder, the second is the destination folder.
    public static void Main(string[] args)
    {
        var baseDirectory = Directory.GetCurrentDirectory();

        var projectRoot = Directory.GetParent(Directory.GetParent(Directory.GetParent(baseDirectory)?.FullName!)?.FullName!)?.FullName;

        var sourceFolder = Path.Combine(projectRoot, "src");
        var destinationFolder = Path.Combine(projectRoot, "dest");

        StartNoThreadTest(sourceFolder, destinationFolder);

        StartThreadTest(sourceFolder, destinationFolder, 3);
    }

    private static void CopyFiles(string sourceFolder, string destinationFolder)
    {
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        var fileNames = Directory.GetFiles(sourceFolder);
        foreach (var sourceFileName in fileNames)
        {
            var destinationFileName = Path.Combine(destinationFolder, Path.GetFileName(sourceFileName));
            lock (_o)
            {
                if (!File.Exists(destinationFileName))
                {
                    File.Copy(sourceFileName, destinationFileName, true);
                    _countCopiedFiles++;
                }
            }
        }
        //
        // var directoryNames = Directory.GetDirectories(sourceFolder);
        // foreach (var sourceDirectoryName in directoryNames)
        // {
        //     var destinationDirectoryName = Path.Combine(destinationFolder, Path.GetDirectoryName(sourceDirectoryName)!);
        //     CopyFiles(sourceDirectoryName, destinationDirectoryName, out var countFiles);
        //     countCopiedFiles += countFiles;
        // }
    }

    private static void StartNoThreadTest(string sourceFolder, string destinationFolder)
    {
        var stopwatch = new Stopwatch();
        if (Directory.Exists(destinationFolder))
        {
            Directory.Delete(destinationFolder, true);
        }
        _countCopiedFiles = 0;
        
        stopwatch.Start();

        CopyFiles(sourceFolder, destinationFolder);

        stopwatch.Stop();
        
        Console.WriteLine($"Method execution time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Quantity of copied files: {_countCopiedFiles}\n");
    }

    private static void StartThreadTest(string sourceFolder, string destinationFolder, int numberOfTasks)
    {
        var stopwatch = new Stopwatch();
        if (Directory.Exists(destinationFolder))
        {
            Directory.Delete(destinationFolder, true);
        }
        _countCopiedFiles = 0;
        
        stopwatch.Start();

        var taskQueue = new TaskQueue.TaskQueue(numberOfTasks);
        for (var i = 0; i < numberOfTasks; i++)
        {
            taskQueue.NewTask(() => { CopyFiles(sourceFolder, destinationFolder); });
        }
        taskQueue.WaitAll();

        stopwatch.Stop();
        
        Console.WriteLine($"Method execution time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Quantity of copied files: {_countCopiedFiles}");
        Console.WriteLine($"Number of tasks: {numberOfTasks}\n");
    }
}