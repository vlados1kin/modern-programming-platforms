using SPP.LaboratoryWork3;

//Task1(@"D:\PROJECTS\SpecialProgrammingPlatforms\SPP.LaboratoryWork3\SPP.LaboratoryWork3\bin\Debug\net9.0\TestLibrary.dll");
Task2("text.txt");
return;

void Task1(string dllPath)
{
    var assemblyFields = AssemblyLoader.GetAllPublicTypes(dllPath);
    foreach (var assemblyField in assemblyFields)
        Console.WriteLine($"{assemblyField.Namespace} - {assemblyField.Name}");
}

void Task2(string filePath)
{
    var logBuffer = new LogBuffer(filePath, 3, 4000);
    while (true)
    {
        var message = Console.ReadLine();
        logBuffer.LogMessage(message ?? string.Empty);
    }
}