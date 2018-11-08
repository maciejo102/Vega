using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Contract.Models;

namespace Vega.Contract
{
    public interface IPhotoDao
    {
        Task<IEnumerable<Photo>> GetPhotosDbSet(int vehicleId);
    }
}