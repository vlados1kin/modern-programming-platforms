using Helper;

namespace TestAssembly;

[ExportClass("public export TestClass")]
public class TestClass;

[ExportClass("internal export AnotherClass")]
internal class AnotherClass;

public class InheritedClass : TestClass;

[ExportClass("public export InheritedClass")]
public class InheritedExportClass : TestClass;

public class SimpleClass;

internal class Class;