using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Contract;
using Vega.Contract.Models;
using Vega.Persistence;

namespace Vega.Persistence
{
    public class PhotoDao : IPhotoDao
    {
        public PhotoDao(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotosDbSet(int vehicleId)
        {
            return await context.Photos
                        .Where(photo => photo.VehicleId == vehicleId)
                        .ToListAsync();
        }


        private readonly VegaDbContext context;
    }
}