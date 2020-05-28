using System;
using System.Threading.Tasks;

namespace EControle.Data.SDK
{
    public interface IWorker : IDisposable
    {
        Task<bool> Complete();
    }
}