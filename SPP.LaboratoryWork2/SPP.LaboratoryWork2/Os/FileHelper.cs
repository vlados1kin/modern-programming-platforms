using System.Text;

namespace SPP.LaboratoryWork2.Os;

public class FileHelper
{
    public static OsHandle? CreateFileHandle(string filePath, uint access, uint creationDisposition)
    {
        var fileHandle = OsHelper.CreateFile(filePath, access, 0, IntPtr.Zero, creationDisposition, 0, IntPtr.Zero);
        
        return fileHandle == IntPtr.Zero ? null : new OsHandle(fileHandle);
    }

    public static void ReadContent(OsHandle osHandle, Action<string>? action = null)
    {
        var buffer = new byte[1024];
        if (!OsHelper.ReadFile(osHandle.Handle, buffer, (uint)buffer.Length, out var bytesRead, IntPtr.Zero))
        {
            return;
        }

        var content = Encoding.UTF8.GetString(buffer, 0, (int)bytesRead);
        if (action != null)
        {
            action.Invoke(content);
        }
        else
        {
            Console.WriteLine(action);
        }
    }
}