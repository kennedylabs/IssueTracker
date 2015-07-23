using System;

namespace IssueTrackerCommon.Infrastructure
{
    public abstract class DisposableBase : IDisposable
    {
        bool _isDisposed;
        object _lock = new object();

        ~DisposableBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void DisposeResources();

        protected virtual void DisposeUnmanagedResources()
        {
        }

        void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (_isDisposed) return;
                else _isDisposed = true;
            }

            if (disposing) DisposeResources();
            DisposeUnmanagedResources();
        }
    }
}
