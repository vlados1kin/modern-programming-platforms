using System.Reflection;

namespace SPP.LaboratoryWork3;

public class AssemblyLoader
{
    public static AssemblyTypes[] GetAllPublicTypes(string dllPath) => 
        Assembly.LoadFrom(dllPath)
            .GetExportedTypes()
            .OrderBy(t => t.Namespace)
            .ThenBy(t => t.Name)
            .Select(t => new AssemblyTypes(t.Name, t.Namespace))
            .ToArray();
}

public record AssemblyTypes(string? Name, string? Namespace);