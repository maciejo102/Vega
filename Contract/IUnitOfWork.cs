using System.Threading.Tasks;

namespace Vega.Contract
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}