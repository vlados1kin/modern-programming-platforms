using System.Reflection;
using Helper;

Task1();
//Task2("D:\\PROJECTS\\SpecialProgrammingPlatforms\\SPP.LaboratoryWork4\\TestAssembly\\bin\\Debug\\net9.0\\TestAssembly.dll");
return;

void Task1()
{
    ParallelExtensions.WaitAll([Do, Do, Do, Do, Do]);
    return;

    void Do(int i) => Console.WriteLine(i);
}

void Task2(string path)
{
    var assembly = Assembly.LoadFrom(path);
    var info = assembly
        .GetExportedTypes()
        .Where(i => i.GetCustomAttribute<ExportClassAttribute>() != null)
        .Select(i => new
        {
            i.FullName,
            i.GetCustomAttribute<ExportClassAttribute>()?.Description
        });
    foreach (var i in info) Console.WriteLine($"{i.FullName} - {i.Description}");
}
