namespace SPP.LaboratoryWork2.Os;

public class OsHandle : IDisposable
{
    private IntPtr _handle;
    private bool _disposed;

    public IntPtr Handle
    {
        get => _handle;
        set
        {
            if (_handle != IntPtr.Zero)
                ReleaseHandle();
            _handle = value;
        }
    }

    public OsHandle(IntPtr handle)
    {
        _handle = handle;
    }

    ~OsHandle()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
            }
            
            ReleaseHandle();
            _disposed = true;
        }
    }

    private void ReleaseHandle()
    {
        if (_handle != IntPtr.Zero)
        {
            OsHelper.CloseHandle(_handle);
            _handle = IntPtr.Zero;
        }
    }
}