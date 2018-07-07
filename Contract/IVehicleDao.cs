using System.Threading.Tasks;
using Vega.Contract.Models;

namespace Vega.Contract
{
    public interface IVehicleDao
    {
        void Add(Vehicle vehicle);
        Task<Vehicle> GetVehicleDbData(int id, bool includeRelated = true);
        void Remove(Vehicle vehicle);
    }
}